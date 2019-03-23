
Cart = {
    //конфигурация
    _properties: {

        // Ссылка на метод добавления товара в корзину (на сервере)
        addToCartLink: '',

        // Ссылка на получение представления корзины (на сервере)
        getCartViewLink: '',

        // Ссылка на удаление товара из корзины
        removeFromCartLink: ''
    },

    // Инициализация логики
    init: function (properties) {
        // Копируем свойства
        $.extend(Cart._properties, properties);

        // Инициализируем перехват события
        Cart.initEvents();
    },

    // Перехват нажатия на кнопку «Добавить в корзину»
    initEvents: function () {
        //найти элементы <a> с классом callAddToCart и подцепить метод addToCart по клику (с помощью jQuery)
        $('a.callAddToCart').on("click", Cart.addToCart);

        //удаление товара из корзины
        $('a.cart_quantity_delete').on("click", Cart.removeFromCart);
    },

    // Событие добавления товара в корзину
    addToCart: function (event) {
        //текущий элемент
        var button = $(this);

        // Отменяем дефолтное действие (действие по-умолчанию)
        event.preventDefault();

        // Получение идентификатора товара из атрибута data-id элемента
        var id = button.data('id');

        // Вызов метода контроллера ajax запрос
        $.get(Cart._properties.addToCartLink + '/' + id)
            //удачное выполнение запроса - функция
            .done(function () {

                // Отображаем сообщение, что товар добавлен в корзину
                Cart.showToolTip(button);

                // В случае успеха – обновляем представление
                Cart.refreshCartView();
            })

            //неудачное выполнение запроса - функция
            .fail(function () {
                //вывод на консоль в лог
                console.log('addToCart error');
            });
    },

    //обновляем представление о состоянии корзины
    refreshCartView: function () {
        // Получаем контейнер корзины (элемент хранит информацию о корзине)
        var container = $("#cartContainer");

        // Получение представления корзины ajax запрос
        $.get(Cart._properties.getCartViewLink)
            .done(function (result) {
                // Обновление html (положить полученные данные в контейнер)
                container.html(result);
            })
            .fail(function () {
                console.log('refreshCartView error');  //вывод на консоль в лог
            });
    },

    //всплывающая подсказка о добавлении товара в корзину
    showToolTip: function (button) {
        // Отображаем тултип
        button.tooltip({ title: "Добавлено в корзину" })  //устанавливаем
            .tooltip('show');                             //отображаем

        // Дестроим его через 0.5 секунды по таймеру
        setTimeout(function () {
            button.tooltip('destroy');
        }, 500);
    },

    //удаление товара из корзины
    removeFromCart: function (event) {
        //текущий элемент
        var button = $(this);

        // Отменяем дефолтное действие
        event.preventDefault();

        // Получение идентификатора из атрибута
        var id = button.data('id');

        // Вызов метода контроллера ajax запрос
        $.get(Cart._properties.removeFromCartLink + '/' + id)
            .done(function () {

                //удалить текущий элемент <tr> - строку
                button.closest('tr').remove();

                // В случае успеха – обновляем представление
                Cart.refreshCartView();
            })
            .fail(function () {
                //вывод на консоль в лог
                console.log('removeFromCart error');
            });
    }

};
