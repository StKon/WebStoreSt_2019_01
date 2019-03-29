ProductItems = {
    //адрес запроса
    _options: {
        getUrl: ''
    },

    //инициализация
    init: function (options) {
        //копируем в ProductItems._options
        $.extend(ProductItems._options, options);

        //привязываем функцию clickOnPage к событию 'click'
        $('.pagination li a').on('click', ProductItems.clickOnPage);
    },

    //обработка события
    clickOnPage: function (event) {

        // Отменяем действие по-умолчанию
        event.preventDefault();

        //если свойство href есть
        if ($(this).prop('href').length > 0) {

            //содержание атрибута page
            var page = $(this).data('page');

            //находим элемент с id itemsContainer и показываем индикатор загрузки
            $('#itemsContainer').LoadingOverlay('show'); // Показываем overlay

            // Получаем все атрибуты
            var data = $(this).data(); 

            // Строим строку запроса
            var query = '';
            for (var key in data) {
                if (data.hasOwnProperty(key)) {
                    query += key + '=' + data[key] + '&';
                }
            }

            // Делаем запрос на сервер
            $.get(ProductItems._options.getUrl + '?' + query)
                .done(function (result) {
                    // Заполняем результат и убираем overlay
                    $('#itemsContainer').html(result);
                    $('#itemsContainer').LoadingOverlay('hide');

                    // Обновляем пейджинг
                    $('.pagination li').removeClass('active');
                    $('.pagination li a').prop('href', '#');

                    //Ищем у .pagination li a в которых атрибут data-page = page. Удпляем из него атрибут href. 
                    //У родительского элемента удаляем класс active.
                    var el = $('.pagination li a[data-page=' + page + ']');
                    el.removeAttr('href')
                    var parel = el.parent();
                    parel.addClass('active');

                }).fail(function () {
                    console.log('clickOnPage getItems error');
                    $('#itemsContainer').LoadingOverlay('hide');
                });
        }
    }
}