$(document).ready(function () {

    // podaci od interesa
    var host = window.location.host;
    var formAction = "Create";
    var editingId;

    // pripremanje dogadjaja za brisanje
    $("body").on("click", "#btnDelete", obrisiAutomobil);

    // priprema dogadjaja za izmenu
    $("body").on("click", "#btnEdit", editAutomobil);

    var token = null;

    var headers = {};

    // Ucitavamo listu kategorija
    var marke = [];
    ucitajMarke();

    // posto inicijalno nismo prijavljeni, sakrivamo odjavu
    $("#odjava").css("display", "none");

    // prikaz login ili register forme
    $("#formRegister").css("display", "none"); // default on load
    $("#btnRegister2").click(function (e) {
        e.preventDefault();
        $("#formLogin").css("display", "none");
        $("#formRegister").css("display", "block");
    });

    // prikazujemo produkte po ucitavanju stranice
    prikaziAutomobile();


    // registracija korisnika
    $("#registracija").submit(function (e) {
        e.preventDefault();

        if (validateRegister()) {
            var email = $("#regEmail").val();
            var loz1 = $("#regLoz").val();
            var loz2 = $("#regLoz2").val();

            // objekat koji se salje
            var sendData = {
                "Email": email,
                "Password": loz1,
                "ConfirmPassword": loz2
            };

            $.ajax({
                type: "POST",
                url: 'https://' + host + "/api/Account/Register",
                data: sendData

            }).done(function (data) {
                alert("Uspešna registracija!");
                $("#formRegister").css("display", "none");
                $("#regEmail").val('');
                $("#regLoz").val('');
                $("#regLoz2").val('');
                $("#formLogin").css("display", "block");

            }).fail(function (data) {
                console.log(data);
                alert(data.responseJSON.ModelState[""][0]);
            });
        }
    });


    // prijava korisnika
    $("#prijava").submit(function (e) {
        e.preventDefault();

        if (validateLogin()) {
            var email = $("#priEmail").val();
            var loz = $("#priLoz").val();

            // objekat koji se salje
            var sendData = {
                "grant_type": "password",
                "username": email,
                "password": loz
            };

            $.ajax({
                "type": "POST",
                "url": 'https://' + host + "/Token",
                "data": sendData

            }).done(function (data) {
                console.log(data);
                $("#info").empty().append("Prijavljen korisnik: " + data.userName);
                token = data.access_token;
                $("#prijava").css("display", "none");
                $("#registracija").css("display", "none");
                $("#odjava").css("display", "block");
                // prikazujemo glavnu tabelu
                prikaziAutomobile();
                // prikaz pretrage i glavne forme
                $("#searchDiv").css("display", "block");
                $("#mainFormDiv").css("display", "block");
                $("#filterDiv").css("display", "block");

            }).fail(function (data) {
                alert(data.responseJSON.error_description);
            });
        }
    });


    // odjava korisnika sa sistema
    $("#odjavise").click(function () {
        token = null;
        headers = {};
        $("#prijava").css("display", "block");
        $("#registracija").css("display", "block");
        $("#odjava").css("display", "none");
        $("#info").empty();
        $("#sadrzaj").empty();
        // prikazi produkte
        prikaziAutomobile();
        // sakrivanje forme i pretrage
        $("#searchDiv").css("display", "none");
        $("#mainFormDiv").css("display", "none");
        $("#filterDiv").css("display", "none");
    })


    // ucitavanje liste proizvoda
    function prikaziAutomobile() {
        // korisnik mora biti ulogovan da bi video CRUD operacije
        if (token) {
            headers.Authorization = "Bearer " + token;

            $.ajax({
                "type": "GET",
                "url": "https://" + host + "/api/automobili",
                "headers": headers

            }).done(function (data) {
                prikaziAutomobileCRUD(data, "#sadrzaj");

            }).fail(function (data) {
                alert(data.status + ": " + data.statusText);
            });
        }
        else {
            $.ajax({
                "type": "GET",
                "url": "https://" + host + "/api/automobili",
                "headers": headers

            }).done(function (data) {
                prikaziAutomobileNoAuth(data);

            }).fail(function (data) {
                alert(data.status + ": " + data.statusText);
            });
        }
    }

    // prikazujemo tabelu proizvoda ulogovanim korisnicima
    function prikaziAutomobileCRUD(data, location) {
        var container = $(location);
        container.empty();

        var table = $("<table class=\"table table-bordered table-striped\"></table>");
        var header = $("<thead class=\"bg-primary\"><tr><th>Model</th><th>Cena</th><th>God. Prvog Modela</th><th>Konjskih Snaga</th><th>Marka</th><th>Izmena</th><th>Brisanje</th></tr></thead>");
        table.append(header);
        var tbody = $("<tbody></tbody>");
        for (var i = 0; i < data.length; i++) {
            // prikazujemo novi red u tabeli
            var row = "<tr>";
            // prikaz podataka
            var displayData = "<td>" + data[i].Model + "</td><td>" + data[i].Cena + "</td><td>" + data[i].GodinaProizvodnje + "</td><td>" + data[i].KonjskihSnaga + "</td><td>" + data[i].MarkaNaziv + "</td>";
            // prikaz dugmadi za izmenu i brisanje
            var stringId = data[i].Id.toString();
            var displayEdit = "<td class=\"bg-info\"><button class=\"btn btn-xs btn-default\" id=btnEdit name =" + stringId + ">Izmena</button></td>";
            var displayDelete = "<td class=\"bg-info\"><button class=\"btn btn-xs btn-default\" id=btnDelete name=" + stringId + ">Brisanje</button></td>";
            row += displayData + displayEdit + displayDelete + "</tr>";
            tbody.append(row);
        }
        table.append(tbody);
        container.append(table);
    }

    // prikazujemo tabelu proizvoda neulogovanim korisnicima
    function prikaziAutomobileNoAuth(data) {
        var container = $("#sadrzaj");
        container.empty();

        var table = $("<table class=\"table table-bordered table-striped\"></table>");
        var header = $("<thead class=\"bg-primary\"><tr><th>Model</th><th>Cena</th><th>God. Prvog Modela</th><th>Konjskih Snaga</th><th>Marka</th></tr></thead>");
        table.append(header);
        var tbody = $("<tbody></tbody>");
        for (var i = 0; i < data.length; i++) {
            // prikazujemo novi red u tabeli
            var row = "<tr>";
            // prikaz podataka
            var displayData = "<td>" + data[i].Model + "</td><td>" + data[i].Cena + "</td><td>" + data[i].GodinaProizvodnje + "</td><td>" + data[i].KonjskihSnaga + "</td><td>" + data[i].MarkaNaziv + "</td>";
            row += displayData + "</tr>";
            tbody.append(row);
        }
        table.append(tbody);
        container.append(table);
    }

    // dodavanje novog proizvoda
    $("#mainForm").submit(function (e) {
        // sprecavanje default akcije forme
        e.preventDefault();

        if (validateMainForm()) {
            var Naziv = $("#Model").val();
            var cenaAutomobila = $("#cenaAutomobila").val();
            var godinaProizvodnje = $("#godinaProizvodnje").val();
            var konjskihSnaga = $("#konjskihSnaga").val();
            var selectMarkaId = $("#selectMarkaId").val();
            var httpAction;
            var sendData;
            var url;

            // u zavisnosti od akcije pripremam objekat
            if (formAction === "Create") {

                httpAction = "POST";
                url = "https://" + host + "/api/automobili";
                sendData = {
                    "Model": Naziv,
                    "Cena": cenaAutomobila,
                    "GodinaProizvodnje": godinaProizvodnje,
                    "KonjskihSnaga": konjskihSnaga,
                    "MarkaId": selectMarkaId
                };
            }
            else {
                httpAction = "PUT";
                url = "https://" + host + "/api/automobili/" + editingId.toString();
                sendData = {
                    "Id": editingId,
                    "Model": Naziv,
                    "Cena": cenaAutomobila,
                    "GodinaProizvodnje": godinaProizvodnje,
                    "KonjskihSnaga": konjskihSnaga,
                    "MarkaId": selectMarkaId
                };
            }

            console.log("Objekat za slanje");
            console.log(sendData);

            $.ajax({
                url: url,
                type: httpAction,
                data: sendData,
                headers: headers
            })
                .done(function (data, status) {
                    formAction = "Create";
                    refreshTable();
                })
                .fail(function (data, status) {
                    alert("Greska prilikom dodavanja/izmene automobila!\n\n" + data.responseJSON.Message);
                });
        }

    });

    // izmena produkta
    function editAutomobil() {
        // izvlacimo id
        var editId = this.name;
        // saljemo zahtev da dobavimo taj property
        $.ajax({
            url: "https://" + host + "/api/automobili/" + editId.toString(),
            type: "GET",
            headers: headers
        })
            .done(function (data, status) {
                $("#Model").val(data.Model);
                $("#cenaAutomobila").val(data.Cena);
                $("#godinaProizvodnje").val(data.GodinaProizvodnje);
                $("#konjskihSnaga").val(data.KonjskihSnaga);
                $("#selectMarkaId").val(data.MarkaId);
                editingId = data.Id;
                formAction = "Update";
            })
            .fail(function (data, status) {
                formAction = "Create";
                alert("Greska prilikom izmene!");
            });
    };


    // brisanje proizvoda
    function obrisiAutomobil() {
        // izvlacimo {id}
        var deleteID = this.name;
        // saljemo zahtev 
        $.ajax({
            url: "https://" + host + "/api/automobili/" + deleteID.toString(),
            type: "DELETE",
            headers: headers
        })
            .done(function (data, status) {
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            });
    };


    // pretraga proizvoda
    $("#searchForm").submit(function (e) {
        e.preventDefault();

        if (validateSearchForm()) {
            var minCena = $("#minCena").val();
            var maxCena = $("#maxCena").val();

            sendData = {
                "minCena": minCena,
                "maxCena": maxCena
            };

            $.ajax({
                url: "https://" + host + "/api/automobili/pretraga",
                type: "POST",
                data: sendData,
                headers: headers
            })
                .done(function (data, status) {
                    prikaziAutomobileCRUD(data, "#sadrzaj");
                })
                .fail(function (data, status) {
                    alert("Greska prilikom pretrage!");
                });
        }

    });


    // osvezi prikaz tabele
    function refreshTable() {
        // cistim formu
        $("#Model").val('');
        $("#cenaAutomobila").val('');
        $("#godinaProizvodnje").val('');
        $("#konjskihSnaga").val('');
        // osvezavam
        prikaziAutomobile();
    };


    // ucitaj kategorije u padajucem meniju
    function ucitajMarke() {
        $.ajax({
            url: "https://" + host + "/api/marke",
            type: "GET",
            headers: headers
        })
            .done(function (data, status) {
                var select1 = $("#selectMarkaId");
                var select2 = $("#selectFilterMarkaId"); // ako imamo dva ili vise selekta treba dodati ovde
                $.each(data, function (key, value) {
                    marke.push(new markeLista(value.Id, value.Naziv));
                    var option = "<option value=\"" + value.Id + "\">" + value.Naziv + "</option>";
                    select1.append(option);
                    select2.append(option); // ako imamo dva ili vise selekta treba dodati i ovde
                });
            })
            .fail(function (data, status) {
                alert("Greska prilikom ucitavanja padajuce liste!");
            });
    }

    console.log(marke);

    function markeLista(id, name) {
        this.Id = id,
            this.Naziv = name
    }


    // filtriraj proizvode po kategoriji
    $("#filterForm").submit(function (e) {
        e.preventDefault();

        var idMarke = $("#selectFilterMarkaId").val();

        $.ajax({
            url: "https://" + host + "/api/automobili/filterbymarka?id=" + idMarke,
            type: "GET",
            headers: headers
        })
            .done(function (data, status) {
                prikaziAutomobileCRUD(data, "#sadrzajFilter");
            })
            .fail(function (data, status) {
                alert("Greska prilikom filtriranja!");
            });
    });


});