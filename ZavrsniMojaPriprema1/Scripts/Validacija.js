// Validacija Login forme
function validateLogin() {
    var email = $("#priEmail").val();
    var pass = $("#priLoz").val();

    var regExp = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;

    if (email === "" || email === null || email === undefined) {
        alert("Morate uneti e-mail adresu!");
        return false;
    }
    else if (!regExp.test(email)) {
        alert("E-mail adresa mora biti ispravna!");
        return false;
    }

    if (pass === "" || pass === null || pass === undefined) {
        alert("Morate uneti lozinu!");
        return false;
    }

    return true;
}

// Validacija Register forme
function validateRegister() {
    var email = $("#regEmail").val();
    var pass = $("#regLoz").val();
    var pass2 = $("#regLoz2").val();

    var regExp = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;

    if (email === "" || email === null || email === undefined) {
        alert("Morate uneti e-mail adresu!");
        return false;
    }
    else if (!regExp.test(email)) {
        alert("E-mail adresa mora biti ispravna!");
        return false;
    }

    if (pass === "" || pass === null || pass === undefined) {
        alert("Morate uneti lozinu!");
        return false;
    }
    else if (pass2 === "" || pass2 === null || pass2 === undefined) {
        alert("Morate ponoviti lozinku u drugom polju!");
        return false;
    }

    if (pass !== pass2) {
        alert("Lozinke moraju biti identicne!");
        return false;
    }

    if (pass.length < 6) {
        alert("Lozinka mora imati najmanje 6 karaktera!");
        return false;
    }

    return true;
}

// Validacija glavne forme
function validateMainForm() {
    var naziv = $("#Model").val();
    var cenaProizvoda = $("#cenaAutomobila").val();
    var godinaProizvodnje = $("#godinaProizvodnje").val();
    var konjskihSnaga = $("#konjskihSnaga").val();
    var markaId = $("#selectMarkaId").val();

    if (naziv === "" || naziv === null || naziv === undefined) {
        alert("Morate uneti naziv proizvoda!");
        return false;
    }
    else if (cenaProizvoda === "" || cenaProizvoda === null || cenaProizvoda === undefined) {
        alert("Morate uneti cenu proizvoda!");
        return false;
    }
    else if (cenaProizvoda < 0) {
        alert("Cena proizvoda mora biti pozitivan broj!");
        return false;
    }

    if (godinaProizvodnje === "" || godinaProizvodnje === null || godinaProizvodnje === undefined) {
        alert("Morate uneti godinu proizvodnje!");
        return false;
    }
    else if (godinaProizvodnje < 1970 || godinaProizvodnje > 2020) {
        alert("Godina proizvodnje mora biti izmedju 1970 i 2020!");
        return false;
    }
    else if (godinaProizvodnje % 1 !== 0) {
        alert("Godina proizvodnje mora biti ceo broj!");
        return false;
    }

    if (konjskihSnaga === "" || konjskihSnaga === null || konjskihSnaga === undefined) {
        alert("Morate uneti broj konjskih snaga!");
        return false;
    }
    else if (konjskihSnaga > 500) {
        alert("Broj konjskih snaga ne moze biti veci od 500!");
        return false;
    }
    else if (konjskihSnaga % 1 !== 0) {
        alert("Broj konjskih snaga mora biti ceo broj!");
        return false;
    }

    return true;
}


// Validacija search forme
function validateSearchForm() {
    var minCena = $("#minCena").val();
    var maxCena = $("#maxCena").val();

    if (minCena === "" || minCena === null || minCena === undefined) {
        alert("Morate uneti minimalnu cenu!");
        return false;
    }
    else if (minCena < 0) {
        alert("Minimalna cena automobila mora biti pozitivan broj!");
        return false;
    }

    if (maxCena === "" || maxCena === null || maxCena === undefined) {
        alert("Morate uneti maksimalnu cenu!");
        return false;
    }
    else if (maxCena < 0) {
        alert("Maksimalna cena automobilaa mora biti pozitivan broj!");
        return false;
    }

    return true;
}