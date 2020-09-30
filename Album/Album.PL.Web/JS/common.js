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
    $('#photo_delete').click(PhotoDelete);

    $('#input_tag').keyup(TagInput);
    $('#tags_dropdown').click(TagOnSelect);
    AddEventToMainPhoto();
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
            AddEventToMainPhoto();
        });
}

function AddEventToMainPhoto() {
    $('.photo_edit_btn').click(function () {
        let img = $(this).parents('.photo_wrapper').find('.embed-responsive-item');
        let id = $(img).attr('id');
        $('#modal_photo_image').attr('src', $(img).attr('src'));
        EditPhoto(null, id);
    });
}

function EditPhoto(event, photoId = null) {
    if (photoId == null) { // Новая фотка
        $('#photo_delete').hide();
        $('#btn_modal_add_photo').show();
        $('#modal_photo_image').removeAttr('src');
        $('#btn_modal_add_photo').html('Добавить фото');
        $('#photo_tag_row').find('.tag_wrapper').remove();
        $('#input_tag').val('');
        $('#photo_photo_id').val('');
    }
    else { // Редактирование существующей фотки
        $('#photo_delete').show();
        $('#btn_modal_add_photo').hide();
        $('#photo_photo_id').val(photoId);
        $.post("/Pages/getTagsByPhotoId.cshtml",
            { photoId: photoId },
            function (data) {
                $('#photo_tag_row').find('.tag_wrapper').remove();
                $('#photo_tag_row').append(data);
                $('.tag_delete').click(function () {
                    $(this).parent().remove();
                });
            });
    }
    
    $('#photoEditModal').modal('show');
}

function PhotoSave() {
    let photoId = $('#photo_photo_id').val();
    var myFormData = new FormData();
    myFormData.append('file', document.getElementById('photo_file').files[0]);
    myFormData.append('photoId', photoId);

    let tags = [];
    let i = 0;
    $('.tag_text').each(function () {
        tags[i++] = $(this).text();
    });
    myFormData.append('tags', tags);

    $.ajax({
        url: "/Pages/photoSave.cshtml",
        type: 'POST',
        processData: false,
        contentType: false,
        dataType: 'json',
        data: myFormData,
        success: function (data) {
            if (data == 0) {
                $("#photoEditModal").modal('hide');
                if (photoId == "")
                    ShowMessage("Фото успешно добавлено");
                else
                    ShowMessage("Фото успешно обновлено");
            }
        }
    });
}

function PhotoDelete() {
    let photoId = $('#photo_photo_id').val();
    $('#confirm_body').html("Вы действительно хотите удалить это фото?");

    $('#confirm_delete_but').click(function () {
        $.post("/Pages/photoDelete.cshtml",
            {
                photoId: photoId
            },
            function (data) {
                if (data == "") {
                    ShowMessage("Успешно удалено");
                    $("#photoEditModal").modal('hide');
                }
                else
                    ShowMessage(data);
                $('#confirm_delete').modal('hide');
                // TODO UpdatePhotoList();
            });
    })
    $('#confirm_delete').modal('show');
}

function TagInput(event) {
    let subString = $('#input_tag').val();

    $('#input_tag').val($('#input_tag').val().match("[0-9A-Za-zА-Яа-яЁё \-]+"));

    if (event.key === 'Enter' || event.keyCode === 13) {
        AddTag(subString);
        $('#input_tag').val("");
        return;
    }

    $.post("/Pages/getTags.cshtml",
        { SubString: subString },
        function (data) {
            $('#tags_dropdown').html(data);
        });
}

function TagOnSelect(event) {
    if ($(event.target).hasClass('tag_to_select')) {
        $('#input_tag').val("");
        $('#tags_dropdown').html("");

        AddTag($(event.target).html(), $(event.target).id);
    }
}

function AddTag(name) {
    if ($('.tag_text').length > 10)
        return;
    let isContains = false;
    $('.tag_text').each(function (index) {
        if ($(this).text() == name)
            isContains = true;;
    });

    if (!isContains) {
        CreateTag(name);
    }
}

function CreateTag(name) {
    let tagWrapper = document.createElement('div');
    $(tagWrapper).addClass('tag_wrapper');
    let tagText = document.createElement('div');
    $(tagText).addClass('tag_text');
    $(tagText).html(name);
    $(tagWrapper).append(tagText);
    let tagDelete = document.createElement('div');
    $(tagDelete).addClass('tag_delete');
    $(tagDelete).html('&times');
    $(tagDelete).click(function () {
        $(this).parent().remove();
    });
    $(tagWrapper).append(tagDelete);
    $('#photo_tag_row').append(tagWrapper);
}