
(function ($) {
    jQuery(function ($) {
        $(".campoCPF").mask("999.999.999-99");
        $(".campoCEP").mask("99999-999");
        $(".campoTelefone").mask("(99) 9999?9-9999").on("blur", function () {
            var last = $(this).val().substr($(this).val().indexOf("-") + 1);

            if (last.length === 3) {
                $(this).val($(this).val().substr(0, 9) + '-' + $(this).val().substr($(this).val().indexOf("-") - 1, 1) + last);
            }
        });
    });
})(jQuery);
