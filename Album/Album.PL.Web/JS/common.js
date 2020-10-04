const no_avatar = "/Images/no_avatar.png";

$(document).ready(Ready);
document.curViewType = null;
document.curUserId = null;


function Ready() {
    $('#btnLogin').click(Login);
    $('#btnLogout').click(Logout);
    $('#modal_file').change(UploadAvatar);
    $('#modal_save').click(UserSave);
    $('#modal_delete').click(UserDelete);
    UserInfoEvents();
    UserListEvents();
    
    $('#photo_file').change(UploadPhoto);
    $('#photo_save').click(PhotoSave);
    $('#photo_delete').click(PhotoDelete);

    $('#input_tag').keyup(TagInput);
    $('.tags_dropdown').click(TagOnSelect);
    $('#findText').keyup(function () {
        let text = $(this).val();
        let dest = $('#main_tags_dropdown');
        GetTagsList(text, dest);
    });
    MainPhotoEvents();
}

function UserListEvents() {
    $('.btn_user_edit').click(UserEdit);
    $('.main_users_row').click(function () {
        document.curViewType = "user";
        document.curUserId = $(this).attr('id');
        ShowPhotos();
    });
}

function MainPhotoEvents() {
    $('.photo_edit_btn').click(function () {
        let img = $(this).parents('.photo_wrapper').find('.embed-responsive-item');
        let id = $(img).attr('id');
        $('#modal_photo_image').attr('src', $(img).attr('src'));
        EditPhoto(null, id);
    });
    $('.btn_add_comment').click(AddComment);
    $('.btn_del_comment').click(DelComment);
    $('.my_rating_modal').click(SetRating);
}

function UserInfoEvents() {
    $('.btn_user_edit').click(UserEdit);
    $('#btn_add_photo').click(EditPhoto);
    $('#btn_my_album').click(function () {
        document.curViewType = "my";
        ShowPhotos();
    });
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
        id = $('#my_id').val();
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
            UserInfoEvents();
        });
}

function UpdateUsersList() {
    $.post("/Pages/mainUsersPartial.cshtml",
        null,
        function (data) {
            $('#main_users').html(data);
            UserListEvents();
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
                ShowPhotos();
            });
    })
    $('#confirm_delete').modal('show');
}

function ShowPhotos(tags = null) {
    let type = document.curViewType;
    let userId = document.curUserId;
    $.post("/Pages/mainPhotoPartial.cshtml",
        {
            type: type,
            userId: userId,
            tags: tags
        },
        function (data) {
            $('#main_photo').fadeOut('fast', function () {
                $('#main_photo').html(data);
                $('#main_photo').fadeIn('fast');
                MainPhotoEvents();
            });
        });
}

function EditPhoto(event, photoId = null) {
    $('#input_tag').val('');
    $('.photo_row').find('.tag_wrapper').remove();
    $('#photo_tags_dropdown').html('');

    if (photoId == null) { // Новая фотка
        $('#photo_delete').hide();
        $('#btn_modal_add_photo').show();
        $('#modal_photo_image').removeAttr('src');
        $('#btn_modal_add_photo').html('Добавить фото');
        $('#photo_photo_id').val('');
    }
    else { // Редактирование существующей фотки
        $('#photo_delete').show();
        $('#btn_modal_add_photo').hide();
        $('#photo_photo_id').val(photoId);
        $.post("/Pages/getTagsByPhotoId.cshtml",
            { photoId: photoId },
            function (data) {
                $('.photo_row').find('.tag_row').append(data);
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
    $('#photo_tag_row').find('.tag_text').each(function () {
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
                $('#photo_tag_row').find('.tag_wrapper').remove();
                ShowPhotos();
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
                ShowPhotos();
            });
    })
    $('#confirm_delete').modal('show');
}

function TagInput(event) {
    let subString = $('#input_tag').val();

    $('#input_tag').val($('#input_tag').val().match("[0-9A-Za-zА-Яа-яЁё \-]+"));

    if ((event.key === 'Enter' || event.keyCode === 13) && $(this).val() != "") {
        AddTag(subString.trim(), $(this));
        $('#input_tag').val("");
        return;
    }

    GetTagsList(subString, $('#photo_tags_dropdown'));
}

function GetTagsList(subString, destination) {
    $.post("/Pages/getTags.cshtml",
        { SubString: subString },
        function (data) {
            destination.html(data);
        });
}

function TagOnSelect(event) {
    if ($(event.target).hasClass('tag_to_select')) {
        $(this).prev().val("");
        $(this).html("");

        AddTag($(event.target).html(), $(this));
    }
}

function AddTag(name, source) {
    let limit = 10;
    if (source.parents('#main_find').length > 0)
        limit = 3;
    if ($('.tag_text').length > limit - 1)
        return;
    let isContains = false;
    $('.tag_text').each(function () {
        if ($(this).text() == name)
            isContains = true;
    });

    if (!isContains) {
        CreateTag(name, source);
    }
}

function CreateTag(name, destination) {
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
        if (destination.parents('#main_find').length > 0) {
            FindPhotoByTags();
        }
    });
    $(tagWrapper).append(tagDelete);
    destination.parents('.tag_row').append(tagWrapper);
    if (destination.parents('#main_find').length > 0) {
        FindPhotoByTags();
    }
}

function FindPhotoByTags() {
    let tags = [];
    let i = 0;
    $('#main_find').find('.tag_text').each(function () {
        tags[i++] = $(this).text();
    });
    if (tags.length != 0)
        document.curViewType = 'tag';
    else
        document.curViewType = 'popular';
    ShowPhotos(tags);
}

function AddComment() {
    let textElem = $(this).parents('.add_comment_wrapper').find('.add_comment_text');
    let photoId = $(this).parents('.photo_viewer_modal').find('.photo_modal_id').val();
    if (textElem.val().length != 0) {
        $.post("/Pages/addComment.cshtml",
            {
                text: textElem.val(),
                photoId: photoId
            },
            function (data) {
                if (data == "") {
                    $('.modal').modal('hide');
                    textElem.val('');
                    ShowPhotos();
                }
                else {
                    ShowMessage(data);
                }
            });
        
    }
}

function DelComment() {
    let commentElem = $(this).parents('.photo_comment');
    let id = $(this).siblings('.comment_id').val();

    $('#confirm_body').html("Удалить комментарий?");

    $('#confirm_delete_but').click(function () {
        $.post("/Pages/delComment.cshtml",
            {
                Id: id
            },
            function (data) {
                if (data == "") {
                    commentElem.remove();
                }
                else
                    ShowMessage(data);
                $('#confirm_delete').modal('hide');
            });
    })
    $('#confirm_delete').modal('show');
}

function SetRating() {
    let ratingElem = $(this);
    let rating = ratingElem.html();
    let photoId = ratingElem.parents('.photo_viewer_modal').find('.photo_modal_id').val();

    $.post("/Pages/setRating.cshtml",
        {
            rating: rating,
            photoId: photoId
        },
        function (data) {
            if (data == "") {
                ratingElem.parents('.photo_viewer_modal').find('.my_rating_modal').removeClass('cur_rating');
                ratingElem.addClass('cur_rating');
                ShowMessage("Голос учтён");
            }
            else {
                ShowMessage(data);
            }
        });
}