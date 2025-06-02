// A-Z a-Z 0-9   Space (Optional), Specials (Optional -- Dot Comma &)
function AlphaNumeric(e, IsSpace, IsSpl) {
    var keyCode = e.keyCode ? e.keyCode : e.which;

    if (IsSpace == 1 && keyCode == 32 && (e.target.value.length == 0 || e.target.value.trim().length == 0))
        e.preventDefault();
    else if (!((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (IsSpace == 1 && e.keyCode == 32)
        || (IsSpl == 1 && (keyCode == 38 || keyCode == 44 || keyCode == 46))))
        e.preventDefault();
}


// A-Z a-Z 0-9 ,Specials (Optional -- Dot Colon Hyphen)
function AlphaNumericWithSpl(e, IsSpl) {
    var keyCode = e.keyCode ? e.keyCode : e.which;

    if (!((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122)
        || (IsSpl == 1 && (keyCode == 46 || keyCode == 58 || keyCode == 45))))
        e.preventDefault();
}


// A-Z a-Z    Space (Optional), Specials (Optional -- Dot Comma &)
function Alphabets(e, IsSpace, IsSpl) {
    var keyCode = e.keyCode ? e.keyCode : e.which;

    if (IsSpace == 1 && keyCode == 32 && (e.target.value.length == 0 || e.target.value.trim().length == 0))
        e.preventDefault();
    else if (!((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (IsSpace == 1 && keyCode == 32) ||
        (IsSpl == 1 && (keyCode == 38 || keyCode == 44 || keyCode == 46))))
        (IsSpl == 1 && (keyCode == 38 || keyCode == 44 || keyCode == 46))
        e.preventDefault();
}


// 0-9    Space (Optional)   Plus (Optional), Specials (Optional -- Dot Comma &)
function Numbers(e, IsSpace, IsPlus, IsSpl) {
    var keyCode = e.keyCode ? e.keyCode : e.which;
    if (IsSpace == 1 && keyCode == 32 && (e.target.value.length == 0 || e.target.value.trim().length == 0))
        e.preventDefault();
    else if (!((keyCode >= 48 && keyCode <= 57) || (IsPlus == 1 && keyCode == 43) || (IsSpace == 1 && keyCode == 32) ||
        (IsSpl == 1 && (keyCode == 46 || keyCode == 44 || keyCode == 38))))
        e.preventDefault();
}


// 0-9 , Specials (Optional -- Dot) ==> Latitude and Longitude
function NumbersWithDot(e,IsSpl) {
    var keyCode = e.keyCode ? e.keyCode : e.which;

    if (!((keyCode >= 48 && keyCode <= 57) || (IsSpl == 1 && keyCode == 46 )))
        e.preventDefault();
}


// Accepts Alphabets & Numbers & Specials --> 0-9  A-Z  a-z  other char--> @-_.  only  [Is Multi Email, separated by ;]
function isEmail(evt, IsMultiEmail) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;

        if (charCode == 8 || charCode == 9)
            return true;

        if ((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) ||
            charCode == 64 || charCode == 45 || charCode == 95 || charCode == 46 || (IsMultiEmail == 1 && charCode == 59))
            return true;

        return false;
    }
    catch (e) {
        return false;
    }
}



// A-Z a-Z    Space (Optional) -- used for EmpName
function EmpName(e) {
    var keyCode = e.keyCode ? e.keyCode : e.which;

    if (!((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || keyCode == 32 || keyCode == 46))
        e.preventDefault();
}



// A-Z a-Z 0-9 Dot Space Hyphan Slash(/) only allowed
function MasterCode(e) {
    var keyCode = e.keyCode ? e.keyCode : e.which;

    if (!((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || e.keyCode == 45 || e.keyCode == 32 ||
        e.keyCode == 46 || e.keyCode == 47))
        e.preventDefault();
}



// A-Z a-Z 0-9 ()[] @ & Hyphen SingleQuote Comma  Dot Space (Optional)  -- Used For OrgName / ItemName, etc ...
function MasterDesc(e) {
    var keyCode = e.keyCode ? e.keyCode : e.which;

    if (!((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || e.keyCode == 45 || e.keyCode == 32 ||
        e.keyCode == 46 || e.keyCode == 47 || e.keyCode == 40 || e.keyCode == 41 || e.keyCode == 91 || e.keyCode == 93 || e.keyCode == 64 || e.keyCode == 44 ||
        e.keyCode == 37 || e.keyCode == 38))
        e.preventDefault();
}


// 0-9  ;  DialCode ==> + Allowed.
function MobileNo(e, IsDailCode) {
    var keyCode = e.keyCode ? e.keyCode : e.which;
    if (!((keyCode >= 48 && keyCode <= 57) || (IsDailCode == 1 && keyCode == 43)))
        e.preventDefault();
}



// 0-9 Hyphen Space Comma  -- Multi Phone Numbers Separated by comma
function PhoneNumbers(e, IsPlus, IsMultiPhone) {
    var keyCode = e.keyCode ? e.keyCode : e.which;
    if (!((keyCode >= 48 && keyCode <= 57) || e.keyCode == 45 || keyCode == 32 || (IsMultiPhone == 1 && keyCode == 44) || (IsPlus == 1 && keyCode == 43)))
        e.preventDefault();
}


function enforceNumberValidation(ele) {
    if ($(ele).data('decimal') != null) {
        // found valid rule for decimal
        var decimal = parseInt($(ele).data('decimal')) || 0;
        var val = $(ele).val();
        if (decimal > 0) {
            var splitVal = val.split('.');
            if (splitVal.length == 2 && splitVal[1].length > decimal) {
                // user entered invalid input
                $(ele).val(splitVal[0] + '.' + splitVal[1].substr(0, decimal));
            }
        } else if (decimal == 0) {
            // do not allow decimal place
            var splitVal = val.split('.');
            if (splitVal.length > 1) {
                // user entered invalid input
                $(ele).val(splitVal[0]); // always trim everything after '.'
            }
        }
    }
}