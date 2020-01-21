// Multistep form navigation
function Step1() {
    $(".step").addClass("d-none");
    $("#step1").removeClass("d-none");
    $(".progressbar li").removeClass("active");
    $(".progressbar li").eq(0).addClass("active");
    scrolltotop();
}
function Step2() {
    $(".step").addClass("d-none");
    $("#step2").removeClass("d-none");
    $(".progressbar li").removeClass("active");
    $(".progressbar li").eq(0).addClass("active");
    $(".progressbar li").eq(1).addClass("active");
}
function Step3() {
    $(".step").addClass("d-none");
    $("#step3").removeClass("d-none");
    $(".progressbar li").removeClass("active");
    $(".progressbar li").eq(0).addClass("active");
    $(".progressbar li").eq(1).addClass("active");
    $(".progressbar li").eq(2).addClass("active");
}
function Step4() {
    $(".step").addClass("d-none");
    $("#step4").removeClass("d-none");
    $(".progressbar li").addClass("active");
    $("#step4Date").text(new Date().toString());
}
//script for grouping items
var count = 1;
$(".group-item").click(function () {
    if (!$(this).children().hasClass('fa-check-circle-o')) {
        $(this).parent().parent().toggleClass("table-info");
    }
    if ($("#step2 .table-info").length > 0) {
        $("#step2applytoall").prop("disabled", false);
    }
});
function scrolltotop() {
    $(window).scrollTop(0);
}
function togglepnltable() {
    $("#pnltable").slideToggle();
}
function warningSave() {
    $("#modalWarningSave").modal('toggle');
}
function warningDelete() {
    $("#modalDeleteGroup").modal('toggle');
    $("#modalWarningSave").modal('toggle');
}

