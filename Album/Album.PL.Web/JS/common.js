const no_avatar = "/Images/no_avatar.png";

$(document).ready(Ready);

function Ready() {
    $('#btnLogin').click(Login);
    $('#btnLogout').click(Logout);
    $('.btn_user_edit').click(UserEdit);
    $('#modal_file').change(UploadAvatar);
    $('#modal_save').click(UserSave);
    $('#modal_delete').click(UserDelete);
    $('#btn_my_album').click(function () {
        ShowPhotos("my");
    });
    $('.main_users_row').click(function () {
        ShowPhotos("user", $(this).attr('id'));
    });
    $('#btn_add_photo').click(EditPhoto);
    $('#photo_file').change(UploadPhoto);
    $('#photo_save').click(PhotoSave);
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

    $.getJSON('/Pages/getUserInfo.cshtml', { 'id': id }, function (data) {
        $('#modal_user_login').val(data['Login']);
        $('#modal_user_name').val(data['Name']);
        if (data['Active'] == true)
            $('#modal_user_active').prop('checked', true);
        else
            $('#modal_user_active').prop('checked', false);
        if (data['Avatar'] != null)
            $('#modal_user_image').attr('src', data['Avatar']);
        else
            $('#modal_user_image').attr('src', no_avatar);
    });

    $('#userEditModal').modal('show');
}

function UpdateMyInfo() {
    $.post("/Pages/mainUserInfoPartial.cshtml",
        null,
        function (data) {
            $('#main_my_info').html(data);
            $('.btn_user_edit').click(UserEdit);
            $('#btn_add_photo').click(EditPhoto);
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
        let file = this.files[0];
        let max_width = $('#modal_div_image').width();
        let max_height = $('#modal_div_image').height();

        let reader = new FileReader();

        reader.readAsDataURL(file);
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
        let file = this.files[0];

        var reader = new FileReader();
        reader.onload = function (event) {
            $('#modal_photo_image').attr('src', event.target.result);
        };
        reader.readAsDataURL(file);

        $('#btn_modal_add_photo').html('Изменить фото');
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

function ShowPhotos(type, userId = null) {
    $.post("/Pages/mainPhotoPartial.cshtml",
        {
            type: type,
            userId: userId
        },
        function (data) {
            $('#main_photo').html(data);
        });
}

function EditPhoto(event, photoId = null) {
    if (photoId == null) { // Новая фотка
        $('#photo_delete').hide();
        $('#btn_modal_add_photo').show();
    }
    else { // Редактирование существующей фотки
        $('#photo_delete').show();
        $('#btn_modal_add_photo').hide();
    }
    $('#modal_photo_image').removeAttr('src');
    $('#btn_modal_add_photo').html('Добавить фото');
    $('#photoEditModal').modal('show');
}

function PhotoSave(event, photoId = null) {
    var myFormData = new FormData();
    myFormData.append('file', document.getElementById('photo_file').files[0]);
    myFormData.append('photoId', photoId);

    $.ajax({
        url: "/Pages/photoSave.cshtml",
        type: 'POST',
        processData: false,
        contentType: false,
        dataType: 'json',
        data: myFormData,
        success: function (data) {
            $("#photoEditModal").modal('hide');
            if (photoId == null)
                ShowMessage("Фото успешно добавлено");
            else
                ShowMessage("Фото успешно обновлено");
        }
    });
}