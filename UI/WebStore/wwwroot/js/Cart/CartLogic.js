
Cart = {
    //конфигурация
    _properties: {

        // Ссылка на метод добавления товара в корзину (на сервере)
        addToCartLink: '',

        // Ссылка на получение представления корзины (на сервере)
        getCartViewLink: '',

        // Ссылка на удаление товара из корзины
        removeFromCartLink: '',

        // Ссылка на уменьшение количества товаров
        decrementLink: ''
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

        // Кнопка «+»
        //$('.cart_quantity_up').on('click', Cart.incrementItem);
        $('.cart_quantity_up').click(Cart.incrementItem);

        // Кнопка «-»
        //$('.cart_quantity_down').on('click', Cart.decrementItem);
        $('.cart_quantity_down').click(Cart.decrementItem);
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
    },

    // увеличение количества товара в корзине на 1
    incrementItem: function (event) {
        var button = $(this);

        // Строка товара
        var container = button.closest('tr');

        // Отменяем дефолтное действие
        event.preventDefault();

        // Получение идентификатора из атрибута
        var id = button.data('id');

        // Вызов метода контроллера
        $.get(Cart._properties.addToCartLink + '/' + id)
            .done(function () {
                // Получаем значение
                var value = parseInt($('.cart_quantity_input', container).val());

                // Увеличиваем его на 1
                $('.cart_quantity_input', container).val(value + 1);

                // Обновляем цену
                Cart.refreshPrice(container);

                // В случае успеха – обновляем представление
                Cart.refreshCartView();
            })
            .fail(function () {
                console.log('incrementItem error');
            });
    },

    // уменьшение количества товара в корзине на 1
    decrementItem: function () {
        var button = $(this);

        // Строка товара
        var container = button.closest('tr');

        // Отменяем дефолтное действие        
        event.preventDefault();

        // Получение идентификатора из атрибута
        var id = button.data('id');

        // Вызов метода контроллера
        $.get(Cart._properties.decrementLink + '/' + id)
            .done(function () {
                //количество товара в строке
                var value = parseInt($('.cart_quantity_input', container).val());


                if (value > 1) {
                    // Уменьшаем его на 1
                    $('.cart_quantity_input', container).val(value - 1);

                    //пересчет сумм
                    Cart.refreshPrice(container);
                }
                else {
                    //удалить строку
                    container.remove();

                    //пересчет сумм
                    Cart.refreshTotalPrice();
                }

                // В случае успеха – обновляем представление
                Cart.refreshCartView();
            })
            .fail(function () {
                console.log('decrementItem error');
            });
    },

    //пересчет суммы строки
    refreshPrice: function (container) {
        // Получаем количество
        var quantity = parseInt($('.cart_quantity_input', container).val());

        // Получаем цену
        var price = parseFloat($('.cart_price', container).data('price'));

        // Рассчитываем общую стоимость
        var totalPrice = quantity * price;

        // Для отображения в виде валюты
        var value = totalPrice.toLocaleString('ru-RU', {
            style: 'currency',
            currency: 'RUB'
        });

        // Сохраняем стоимость для поля «Итого»        
        $('.cart_total_price', container).data('price', totalPrice);

        // Меняем значение
        $('.cart_total_price', container).html(value);

        //пересчет итоговой суммы
        Cart.refreshTotalPrice();
    },

    //обновляет сумму заказа
    refreshTotalPrice: function () {
        var total = 0;

        //беруться элементы с классом cart_total_price и цикл по ним
        $('.cart_total_price').each(function () {
            var price = parseFloat($(this).data('price'));
            total += price;
        });

        // Для отображения в виде валюты
        var value = total.toLocaleString('ru-RU', {
            style: 'currency', currency:
                'RUB'
        });

        // Меняем значение элемента с id
        $('#totalOrderSum').html(value);
    }
};
