var cartItems = {};
getCartItems();

function getCartItems() {
    $.getJSON('/CartView/CartsItemsId', function (data) {
        cartItems = data;
        $('#cartItemsCount').text(Object.keys(cartItems).length);
    });
}
    
angular.module('zclone', [])
    .controller('Items', function ($scope, $http) {
        var url = '/api/' + webService + '/?offset=' + offset +
            ((sortBy != '') ? '&sortBy=' + sortBy : '') +
            ((searchFor != '') ? '&searchFor=' + searchFor : '');
        var itemsPromise = $http.get(url);
        itemsPromise.success(function (data, status, headers, config) {
            if (webService == 'Items') {
                for (i in data) {
                    data[i] = addCartButtonUtils(data[i]);
                }
                $scope.items = data;
            } else {
                $scope.items = data.map(function(cart) {
                    return addCartButtonUtils(cart.item);
                });
            }
        });
        $scope.toggleCartOp = function(item) {
            console.log(item);
            if (item.cart_text == 'Drop') {
                dropFromCart(item.id_catalog_config);
            } else {
                addToCart(item.id_catalog_config);
            }
            getCartItems();
        }
    });

function addCartButtonUtils(data) {
    if (data.id_catalog_config in cartItems) {
        data.cart_text = 'Drop';
        data.cart_button_color = 'danger';
    } else {
        data.cart_text = 'Add';
        data.cart_button_color = 'warning';
    }
    return data;
}

function addToCart(item_id) {
    $.ajax({
        url: '/api/Carts/' + item_id,
        type: 'POST',
        success: function (data) {
            if ('Message' in data) {
                var message = '<strong>Oops!</strong> ' + data['Message'];
                $('#alert').empty().append(displayAlert('danger', message));
            } else {
                var message = '<strong>Way to go!</strong> You added <a href="/ItemView/Details/'
                    + data.id_catalog_config + '" class="alert-link">' + data.name + '</a> to your Cart.';
                $('#' + item_id + ' button')
                    .removeClass('btn-warning')
                    .addClass('btn-danger')
                    .attr('onclick', 'dropFromCart("' + item_id + '")')
                    .addClass('dropFromCart')
                    .removeClass('addToCart');
                $('#' + item_id + ' button > span.toggleCartOpText')
                    .text($('.main-item-details').length > 0 ? 'Drop from Cart' : 'Drop');
                $('#alert').empty().append(displayAlert('success', message));
            }
        }
    });
}

function dropFromCart(item_id) {
    $.ajax({
        url: '/api/Carts/' + item_id,
        type: 'DELETE',
        success: function (data) {
            if ('Message' in data) {
                var message = '<strong>Oops!</strong> ' + data['Message'];
                $('#alert').empty().append(displayAlert('danger', message));
            } else {
                var message = '<strong>Heart-breaking!</strong> You dropped this item from your Cart.';
                $('#' + item_id + ' button')
                    .removeClass('btn-danger')
                    .addClass('btn-warning')
                    .attr('onclick', 'addToCart("' + item_id + '")')
                    .addClass('addToCart')
                    .removeClass('dropFromCart');
                $('#' + item_id + ' button > span.toggleCartOpText')
                    .text($('.main-item-details').length > 0 ? 'Add to Cart' : 'Add');
                $('#alert').empty().append(displayAlert('warning', message));
            }
        }
    });
}

function displayAlert(alertType, messageHTML) {
    return '<div class="alert alert-' + alertType + ' alert-dismissible" role="alert">' +
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
        messageHTML + '</div>';
}