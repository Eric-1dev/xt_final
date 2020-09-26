$(document).ready(onReady);

function onReady() {
    $('#loginButton').click(Login);
    $('#registrationButton').click(Registration);
    $('#guestButton').click(Guestlogin);

    $(document).keydown(function (event) {
        if (event.keyCode === 13) {
            $('#loginButton').click();
        }
    });

    $('#login').focus();
}

function Login() {
    $.post("/Pages/loginLogic.cshtml",
        {
            Login: $('#login').val(),
            Password: $('#password').val()
        }, function (data) {
            if (data == "")
                window.location.href = "/";
            else
                $('#result').html(data);
        });
}

function Registration() {
    $.post("/Pages/registerLogic.cshtml",
        {
            Login: $('#login').val(),
            Password: $('#password').val(),
            IsAdmin: $('#is_admin').prop('checked')
        }, function (data) {
            $('#result').html(data);
        });
}

function Guestlogin() {
    window.location.href = "/";
}