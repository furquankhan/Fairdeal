// navigation slide-in
$(window).load(function() {
  $('.nav_slide_button').click(function() {
    $('.pull').slideToggle();
  });
});

var api = {
    call: function (method, info, success, error)
    {

    },
    OnSuccess: function () { },
    OnFail: function () { }
}