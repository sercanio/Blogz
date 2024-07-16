// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    function setTheme(theme) {
        var htmlElement = document.documentElement;
        htmlElement.setAttribute('data-bs-theme', theme);

        if (theme === 'light') {
            $('#sunicon').css('display', 'none');
            $('#moonicon').css('display', 'block');
        } else {
            $('#sunicon').css('display', 'block');
            $('#moonicon').css('display', 'none');
        }
    }

    var savedTheme = localStorage.getItem('theme');
    if (savedTheme) {
        setTheme(savedTheme);
    } else {
        setTheme('dark');
        localStorage.setItem('theme', 'dark');
    }

    $('#themeToggle').on('click', function () {
        var currentTheme = document.documentElement.getAttribute('data-bs-theme');
        var newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        setTheme(newTheme);
        localStorage.setItem('theme', newTheme);
    });
});