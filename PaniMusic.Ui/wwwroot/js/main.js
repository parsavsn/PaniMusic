!(function ($) {
    "use strict";

    // Preloader
    $(window).on('load', function () {
        if ($('#preloader').length) {
            $('#preloader').delay(100).fadeOut('slow', function () {
                $(this).remove();
            });
        }
    });

    //Smooth scroll for the navigation menu and links with .scrollto classes
    var scrolltoOffset = $('#header').outerHeight() - 2;
    $(document).on('click', '.nav-menu a, .mobile-nav a, .scrollto', function (e) {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            if (target.length) {
                e.preventDefault();

                var scrollto = target.offset().top - scrolltoOffset;
                if ($(this).attr("href") == '#header') {
                    scrollto = 0;
                }

                $('html, body').animate({
                    scrollTop: scrollto
                }, 1500, 'easeInOutExpo');

                if ($(this).parents('.nav-menu, .mobile-nav').length) {
                    $('.nav-menu .active, .mobile-nav .active').removeClass('active');
                    $(this).closest('li').addClass('active');
                }

                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                    $('.mobile-nav-overly').fadeOut();
                }
                return false;
            }
        }
    });

    // Activate smooth scroll on page load with hash links
    $(document).ready(function () {
        if (window.location.hash) {
            var initial_nav = window.location.hash;
            if ($(initial_nav).length) {
                var scrollto = $(initial_nav).offset().top - scrolltoOffset;
                $('html, body').animate({
                    scrollTop: scrollto
                }, 1500, 'easeInOutExpo');
            }
        }
    });

    // Mobile Navigation
    if ($('.nav-menu').length) {
        var $mobile_nav = $('.nav-menu').clone().prop({
            class: 'mobile-nav d-lg-none'
        });
        $('body').append($mobile_nav);
        $('body').prepend('<button type="button" class="mobile-nav-toggle d-lg-none"><i class="icofont-navigation-menu"></i></button>');
        $('body').append('<div class="mobile-nav-overly"></div>');

        $(document).on('click', '.mobile-nav-toggle', function (e) {
            $('body').toggleClass('mobile-nav-active');
            $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
            $('.mobile-nav-overly').toggle();
        });

        $(document).on('click', '.mobile-nav .drop-down > a', function (e) {
            e.preventDefault();
            $(this).next().slideToggle(300);
            $(this).parent().toggleClass('active');
        });

        $(document).click(function (e) {
            var container = $(".mobile-nav, .mobile-nav-toggle");
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                    $('.mobile-nav-overly').fadeOut();
                }
            }
        });
    } else if ($(".mobile-nav, .mobile-nav-toggle").length) {
        $(".mobile-nav, .mobile-nav-toggle").hide();
    }

    // Toggle .header-scrolled class to #header when page is scrolled
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#header').addClass('header-scrolled');
        } else {
            $('#header').removeClass('header-scrolled');
        }
    });

    if ($(window).scrollTop() > 100) {
        $('#header').addClass('header-scrolled');
    }

    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });

    $('.back-to-top').click(function () {
        $('html, body').animate({
            scrollTop: 0
        }, 1500, 'easeInOutExpo');
        return false;
    });

    // Skills section
    $('.skills-content').waypoint(function () {
        $('.progress .progress-bar').each(function () {
            $(this).css("width", $(this).attr("aria-valuenow") + '%');
        });
    }, {
        offset: '80%'
    });

    // Porfolio isotope and filter
    $(window).on('load', function () {
        var portfolioIsotope = $('.portfolio-container').isotope({
            itemSelector: '.portfolio-item'
        });

        $('#portfolio-flters li').on('click', function () {
            $("#portfolio-flters li").removeClass('filter-active');
            $(this).addClass('filter-active');

            portfolioIsotope.isotope({
                filter: $(this).data('filter')
            });
            aos_init();
        });


    });

    // Portfolio details carousel
    $(".portfolio-details-carousel").owlCarousel({
        autoplay: true,
        dots: true,
        loop: true,
        items: 1
    });

    // Init AOS
    function aos_init() {
        AOS.init({
            duration: 1000,
            once: true
        });
    }
    $(window).on('load', function () {
        aos_init();
    });

})(jQuery);


$(document).ready(function () {
    // top visited
    $("#music-visit").click(function () {
        $("#music-visit").addClass("select-category");
        $("#album-visit").removeClass("select-category");
        $("#musicvideo-visit").removeClass("select-category");

        $("#music-list-visit").removeClass("hide");
        $("#album-list-visit").addClass("hide");
        $("#musicvideo-list-visit").addClass("hide");
    });

    $("#album-visit").click(function () {
        $("#music-visit").removeClass("select-category");
        $("#album-visit").addClass("select-category");
        $("#musicvideo-visit").removeClass("select-category");

        $("#music-list-visit").addClass("hide");
        $("#album-list-visit").removeClass("hide");
        $("#musicvideo-list-visit").addClass("hide");
    });

    $("#musicvideo-visit").click(function () {
        $("#music-visit").removeClass("select-category");
        $("#album-visit").removeClass("select-category");
        $("#musicvideo-visit").addClass("select-category");

        $("#music-list-visit").addClass("hide");
        $("#album-list-visit").addClass("hide");
        $("#musicvideo-list-visit").removeClass("hide");
    });

    // top rated
    $("#music-rate").click(function () {
        $("#music-rate").addClass("select-category");
        $("#album-rate").removeClass("select-category");
        $("#musicvideo-rate").removeClass("select-category");

        $("#music-list-rate").removeClass("hide");
        $("#album-list-rate").addClass("hide");
        $("#musicvideo-list-rate").addClass("hide");
    });

    $("#album-rate").click(function () {
        $("#music-rate").removeClass("select-category");
        $("#album-rate").addClass("select-category");
        $("#musicvideo-rate").removeClass("select-category");

        $("#music-list-rate").addClass("hide");
        $("#album-list-rate").removeClass("hide");
        $("#musicvideo-list-rate").addClass("hide");
    });

    $("#musicvideo-rate").click(function () {
        $("#music-rate").removeClass("select-category");
        $("#album-rate").removeClass("select-category");
        $("#musicvideo-rate").addClass("select-category");

        $("#music-list-rate").addClass("hide");
        $("#album-list-rate").addClass("hide");
        $("#musicvideo-list-rate").removeClass("hide");
    });
});


$(window).on("load", function () {
    $('.btn-forget').on('click', function (e) {
        e.preventDefault();
        $('.form-items', '.form-content').addClass('hide-it');
        $('.form-sent', '.form-content').addClass('show-it');
    })
});


var newsletterForm = document.getElementById('form-newsletter')

newsletterForm.addEventListener('submit', myPreventDefault)

function myPreventDefault(e) {
    e.preventDefault()
}

//ajax function for send newsletter form ( in the footer of the website )

function sendWithAjax() {

    var firstNameForm = document.getElementById('firstName-newsletter');

    var lastNameForm = document.getElementById('lastName-newsletter');

    var emailForm = document.getElementById('email-newsletter');

    var successDiv = document.getElementById('send-is-success');

    var notSuccessDiv = document.getElementById('send-is-not-success');


    var data = {
        firstName: firstNameForm.value,
        lastName: lastNameForm.value,
        email: emailForm.value
    };

    var jsonData = JSON.stringify(data);

    var xhr = new XMLHttpRequest()

    xhr.open('POST', 'https://localhost:44333/api/newslettermembership/addtonewslettermembership', true)

    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.send(jsonData)

    xhr.onreadystatechange = function () {

        if (this.status == 200) {
            successDiv.innerHTML = `ایمیل شما با موفقیت ثبت شد`;
        }
        else {
            notSuccessDiv.innerHTML = `
ایمیل ثبت نشد ! لطفا به موارد زیر توجه نمایید
<br>
نام و نام خانوادگی باید به درستی پر شود
<br>
نباید ایمیل تکراری وارد شود
`;
        }
    }
}

//ajax function for send track comment form

function sendTrackCommentWithAjax(trackId,userId) {

    var nameForm = document.getElementById('name-comment');

    var commentForm = document.getElementById('comment-comment');

    var rateForm = document.getElementById('rate-comment');

    var successCommentDiv = document.getElementById('send-comment-is-success');

    var notSuccessCommentDiv = document.getElementById('send-comment-is-not-success');


    var data = {
        name: nameForm.value,
        comment: commentForm.value,
        rate: rateForm.value,
        trackId: trackId,
        userId: userId
    };

var jsonData = JSON.stringify(data);

var xhr = new XMLHttpRequest()

xhr.open('POST', 'https://localhost:44333/api/feedbackcrud/createfeedback', true)

xhr.setRequestHeader("Content-Type", "application/json");

xhr.send(jsonData)

xhr.onreadystatechange = function () {

    if (this.status == 200) {
        successCommentDiv.innerHTML = `نظر شما با موفقیت ثبت و پس از تایید ادمین ، نمایش داده می‌شود`;
    }
    else {
        notSuccessCommentDiv.innerHTML = `نظر شما ثبت نشد ! لطفا همه موارد را با دقت تکمیل نمایید`;
    }
}
}

//ajax function for send album comment form

function sendAlbumCommentWithAjax(albumId, userId) {

    var nameForm = document.getElementById('name-comment');

    var commentForm = document.getElementById('comment-comment');

    var rateForm = document.getElementById('rate-comment');

    var successCommentDiv = document.getElementById('send-comment-is-success');

    var notSuccessCommentDiv = document.getElementById('send-comment-is-not-success');


    var data = {
        name: nameForm.value,
        comment: commentForm.value,
        rate: rateForm.value,
        albumId: albumId,
        userId: userId
    };

    var jsonData = JSON.stringify(data);

    var xhr = new XMLHttpRequest()

    xhr.open('POST', 'https://localhost:44333/api/feedbackcrud/createfeedback', true)

    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.send(jsonData)

    xhr.onreadystatechange = function () {

        if (this.status == 200) {
            successCommentDiv.innerHTML = `نظر شما با موفقیت ثبت و پس از تایید ادمین ، نمایش داده می‌شود`;
        }
        else {
            notSuccessCommentDiv.innerHTML = `نظر شما ثبت نشد ! لطفا همه موارد را با دقت تکمیل نمایید`;
        }
    }
}

//ajax function for send musicvideo comment form

function sendMusicVideoCommentWithAjax(musicVideoId, userId) {

    var nameForm = document.getElementById('name-comment');

    var commentForm = document.getElementById('comment-comment');

    var rateForm = document.getElementById('rate-comment');

    var successCommentDiv = document.getElementById('send-comment-is-success');

    var notSuccessCommentDiv = document.getElementById('send-comment-is-not-success');


    var data = {
        name: nameForm.value,
        comment: commentForm.value,
        rate: rateForm.value,
        musicVideoId: musicVideoId,
        userId: userId
    };

    var jsonData = JSON.stringify(data);

    var xhr = new XMLHttpRequest()

    xhr.open('POST', 'https://localhost:44333/api/feedbackcrud/createfeedback', true)

    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.send(jsonData)

    xhr.onreadystatechange = function () {

        if (this.status == 200) {
            successCommentDiv.innerHTML = `نظر شما با موفقیت ثبت و پس از تایید ادمین ، نمایش داده می‌شود`;
        }
        else {
            notSuccessCommentDiv.innerHTML = `نظر شما ثبت نشد ! لطفا همه موارد را با دقت تکمیل نمایید`;
        }
    }
}