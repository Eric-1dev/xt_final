$(document).ready(Ready);

function Ready() {
    $('#logoutButton').click(Logout);
}

function Logout() {
    window.location.href = "logout.cshtml";
}