var baseUrl = 'http://localhost:9876/';

$(function () {
    var connection = $.hubConnection(baseUrl);
    var proxy = connection.createHubProxy('myHub');
    proxy.on('add', function (msg) {
        console.log(msg);
    });
    proxy.on('senddata', function (msgs) {
        console.log('hi');
        viewModel.Users(msgs);
    });
    connection.start()
        .done(function () {
            console.log('Now connected, connection ID=' + connection.id);
            proxy.invoke('send', 'Signal test succeeded...').done(function () {
                console.log('Invocation of signalr server succeeded');
            }).fail(function (error) {
                console.log('Invocation of  signalr server failed. Error: ' + error);
            });
        })
        .fail(function () { console.log('Could not connect'); });
});
var viewModel = {
    Users: ko.observableArray(),
    selectedView: ko.observable("itemTmpl"),
    editItem : function (user) {
        viewModel.selectedUser(user);
    },
    cancelItemEdit : function (user) {
        viewModel.selectedUser(null);
    },
    sorted:ko.observable(true),
    selectedUser: ko.observable(),
    sortItems: function (usrs) {
        var sortedUsers = usrs.Users();
        if (usrs.sorted()) {
            usrs.sorted(false);
            sortedUsers.sort(function (a, b) { return a.Name > b.Name; });
        }
        else {
            usrs.sorted(true);
            sortedUsers.sort(function (a, b) { return b.Name > a.Name; });
        }
        viewModel.Users(sortedUsers);
    },
    DisplayUser: function () {
        $.ajax({
            url: baseUrl + 'get',
            type: 'GET',
            data: {},
            dataType: 'json',
            success: function (data) {
                viewModel.Users.removeAll();
                viewModel.Users(data);
            }
        });
    },
    AddUser: function () {
        var user = new Object();
        user.id = $('#id').val();
        user.name = $('#name').val().toLowerCase().replace(/\b[a-z]/g, function (letter) {
            return letter.toUpperCase();
        });
        user.age = $('#age').val();

        $('#id').val(''); $('#name').val(''); $('#age').val('');
        var token = btoa(user.name + ":" + user.age);
        $.ajax({
            url: baseUrl + 'post',
            type: 'POST',
            data: user,
            dataType: 'json',
            success: function (data) {
                viewModel.Users.unshift(data);
                viewModel.selectedUser(null);
            }
        });
    },
    UpdateUser: function (user) {
        user.Name = user.Name.toLowerCase().replace(/\b[a-z]/g, function (letter) {
            return letter.toUpperCase();
        });
        viewModel.selectedUser(null);
        $.ajax({
            type: "PUT",
            url: baseUrl + 'put',
            data: user,
            dataType: 'json',
            success: function (result) {
                viewModel.selectedUser(result);
                alert("Updated");
            }
        });
    },
    DeleteUser: function (user) {
        viewModel.Users.remove(user);
        $.ajax({
            type: "DELETE",
            url: baseUrl + 'delete?id=' + user.Id,
            data: user,
            dataType: 'json',
            success: function (result) {
                viewModel.selectedUser(null);
                alert("deleted...");
            }
        });
    }
};