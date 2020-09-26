const no_avatar = "/Images/no_avatar.png";

$(document).ready(Ready);

function Ready() {
    $('#btnLogin').click(Login);
    $('#btnLogout').click(Logout);
    $('.btn_user_edit').click(UserEdit);
    $('#modal_file').change(UploadAvatar);
    $('#modal_save').click(UserSave);
    $('#modal_delete').click(UserDelete);
    $('#btn_my_album').click(ShowPhotos);
    $('.main_users_row').click(ShowPhotos);
    $('#btn_add_photo').click(EditPhoto);
    $('#photo_file').change(UploadPhoto);
}

function ShowMessage(data) {
    $('#toast_body').html(data);
    $('.toast').toast('show');
}

function Logout() {
    window.location.href = "logout.cshtml";
}

function Login() {
    window.location.href = "login.cshtml";
}

function UserEdit() {
    event.stopPropagation();
    let id;
    let type;
    if ($(this).hasClass('right_users_edit_but')) {
        type = "otherUser";
        id = $(this).parent().attr('id');
        $('#modal_active').show();
        if ($('#superuser').val() == 1) {
            $('#modal_delete').show();
            $('#modal_save').show();
        }
        else {
            $('#modal_delete').hide();
            $('#modal_save').hide();
        }
    }
    else {
        type = "myInfo";
        id = $(this).attr('id');
        $('#modal_active').hide();
        $('#modal_delete').hide();
        $('#modal_save').show();
    }

    $('#modal_user_id').val(id);
    $('#modal_type').val(type);
    $('#modal_user_image').attr('src', no_avatar);

    $.getJSON('/Pages/getUserInfo.cshtml', { 'id': id }, function (data) {
        $('#modal_user_login').val(data['Login']);
        $('#modal_user_name').val(data['Name']);
        if (data['Active'] == true)
            $('#modal_user_active').prop('checked', true);
        else
            $('#modal_user_active').prop('checked', false);
        if (data['Image'] != null)
            $('#modal_user_image').attr('src', data['Image']);
    });

    $('#userEditModal').modal('show');
}

function UpdateMyInfo() {
    $.post("/Pages/mainUserInfoPartial.cshtml",
        null,
        function (data) {
            $('#main_my_info').html(data);
            $('.btn_user_edit').click(UserEdit);
        });
}

function UpdateUsersList() {
    $.post("/Pages/mainUsersPartial.cshtml",
        null,
        function (data) {
            $('#main_users').html(data);
            $('.btn_user_edit').click(UserEdit);
        });
}

function UploadAvatar() {
    if (this.files && this.files[0]) {
        let max_width = $('#modal_div_image').width();
        let max_height = $('#modal_div_image').height();

        let reader = new FileReader();

        reader.readAsDataURL(this.files[0]);
        reader.onload = function (event) {
            let img = new Image(); // Масштабируем

            img.src = event.target.result;
            img.onload = () => {
                // Масштабируем под новые размеры, сохраняя пропорции
                let scaleFactor = img.height / img.width;
                let new_width = max_width;
                let new_height = new_width * scaleFactor;
                if (new_height > max_height) {
                    new_height = max_height;
                    new_width = new_height / scaleFactor;
                }
                /////////////////////////////////////////////////////
                let elem = document.createElement('canvas');
                elem.width = new_width;
                elem.height = new_height;
                let ctx = elem.getContext('2d');

                ctx.drawImage(img, 0, 0, new_width, new_height);

                $('#modal_user_image').attr('src', elem.toDataURL());
            }
        }
    }
}

function UploadPhoto() {
    if (this.files && this.files[0]) {
        let max_width = parseInt($('#photo_div_image').css('max-width'));
        let max_height = parseInt($('#photo_div_image').css('max-height'));

        let reader = new FileReader();

        reader.readAsDataURL(this.files[0]);
        reader.onload = function (event) {
            let img = new Image(); // Масштабируем

            img.src = event.target.result;
            img.onload = () => {
                // Масштабируем под новые размеры, сохраняя пропорции
                let scaleFactor = img.height / img.width;
                let new_width = max_width;
                let new_height = new_width * scaleFactor;
                if (new_height > max_height) {
                    new_height = max_height;
                    new_width = new_height / scaleFactor;
                }
                /////////////////////////////////////////////////////
                let elem = document.createElement('canvas');
                elem.width = new_width;
                elem.height = new_height;
                let ctx = elem.getContext('2d');

                ctx.drawImage(img, 0, 0, new_width, new_height);

                $('#modal_photo_image').attr('src', elem.toDataURL());
            }
        }
    }
}

function UserSave() {
    let data = {};

    let avatar;
    avatar = $('#modal_user_image').attr('src');
    if (avatar == no_avatar)
        avatar = null;

    data.Id = $('#modal_user_id').val();
    data.Name = $('#modal_user_name').val();
    data.Password = $('#modal_user_pass').val();
    data.Avatar = avatar;
    data.Active = $('#modal_user_active').prop('checked');

    $.post("/Pages/userSave.cshtml",
        data,
        function (data) {
            if (data == "") {
                $("#userEditModal").modal('hide');
                ShowMessage("Данные пользователя успешно обновлены");
                if ($('#modal_type').val() == "myInfo")
                    UpdateMyInfo();
                else
                    UpdateUsersList();
            }
            else
                ShowMessage(data);
        });
}

function UserDelete() {
    let id = $('#modal_user_id').val();
    let name = $('#modal_user_name').val();

    $('#confirm_body').html("Вы действительно хотите удалить пользователя " + name + "? Удалятся все его фотографии.");

    $('#confirm_delete_but').click(function () {
        $.post("/Pages/userDelete.cshtml",
            {
                Id: id
            },
            function (data) {
                if (data == "") {
                    ShowMessage("Успешно удалено");
                    $("#userEditModal").modal('hide');
                }
                else
                    ShowMessage(data);
                $('#confirm_delete').modal('hide');
                UpdateUsersList();
            });
    })
    $('#confirm_delete').modal('show');
}

function ShowPhotos() {
    $.post("/Pages/mainPhotoPartial.cshtml",
        { id: $(this).attr('id') },
        function (data) {
            $('#main_photo').html(data);
        });
}

function EditPhoto() {
    $('#photoEditModal').modal('show');
}