function floatToString(e, t) {
    var n = e.toFixed(t).toString();
    return n.match(/^\.\d+/) ? "0" + n : n
}

function attributeToString(e) {
    return "string" != typeof e && (e += "", "undefined" === e && (e = "")), jQuery.trim(e)
}
"undefined" == typeof window.Shopify && (window.Shopify = {}), Shopify.money_format = "${{amount}}", Shopify.onError = function (e, t) {
    var n = eval("(" + e.responseText + ")");
    n.message ? alert(n.message + "(" + n.status + "): " + n.description) : alert("Error : " + Shopify.fullMessagesFromErrors(n).join("; ") + ".")
}, Shopify.fullMessagesFromErrors = function (e) {
    var t = [];
    return jQuery.each(e, function (e, n) {
        jQuery.each(n, function (n, r) {
            t.push(e + " " + r)
        })
    }), t
}, Shopify.onCartUpdate = function (e) {
    alert("There are now " + e.item_count + " items in the cart.")
}, Shopify.onCartShippingRatesUpdate = function (e, t) {
    var n = "";
    t.zip && (n += t.zip + ", "), t.province && (n += t.province + ", "), n += t.country, alert("There are " + e.length + " shipping rates available for " + n + ", starting at " + Shopify.formatMoney(e[0].price) + ".")
}, Shopify.onItemAdded = function (e) {
    alert(e.title + " was added to your shopping cart.")
}, Shopify.onProduct = function (e) {
    alert("Received everything we ever wanted to know about " + e.title)
}, Shopify.formatMoney = function (e, t) {
    function n(e, t) {
        return "undefined" == typeof e ? t : e
    }

    function r(e, t, r, i) {
        if (t = n(t, 2), r = n(r, ","), i = n(i, "."), isNaN(e) || null == e) return 0;
        e = (e / 100).toFixed(t);
        var o = e.split("."),
            a = o[0].replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1" + r),
            s = o[1] ? i + o[1] : "";
        return a + s
    }
    "string" == typeof e && (e = e.replace(".", ""));
    var i = "",
        o = /\{\{\s*(\w+)\s*\}\}/,
        a = t || this.money_format;
    switch (a.match(o)[1]) {
        case "amount":
            i = r(e, 2);
            break;
        case "amount_no_decimals":
            i = r(e, 0);
            break;
        case "amount_with_comma_separator":
            i = r(e, 2, ".", ",");
            break;
        case "amount_no_decimals_with_comma_separator":
            i = r(e, 0, ".", ",")
    }
    return a.replace(o, i)
}, Shopify.resizeImage = function (e, t) {
    try {
        if ("original" == t) return e;
        var n = e.match(/(.*\/[\w\-\_\.]+)\.(\w{2,4})/);
        return n[1] + "_" + t + "." + n[2]
    } catch (r) {
        return e
    }
}, Shopify.addItem = function (e, t, n) {
    var t = t || 1,
        r = {
            type: "POST",
            url: "/cart/add.js",
            data: "quantity=" + t + "&id=" + e,
            dataType: "json",
            success: function (e) {
                "function" == typeof n ? n(e) : Shopify.onItemAdded(e)
            },
            error: function (e, t) {
                Shopify.onError(e, t)
            }
        };
    jQuery.ajax(r)
}, Shopify.addItemFromForm = function (e, t) {
    var n = {
        type: "POST",
        url: "/cart/add.js",
        data: jQuery("#" + e).serialize(),
        dataType: "json",
        success: function (e) {
            "function" == typeof t ? t(e) : Shopify.onItemAdded(e)
        },
        error: function (e, t) {
            Shopify.onError(e, t)
        }
    };
    jQuery.ajax(n)
}, Shopify.getCart = function (e) {
    jQuery.getJSON("/cart.js", function (t, n) {
        "function" == typeof e ? e(t) : Shopify.onCartUpdate(t)
    })
}, Shopify.pollForCartShippingRatesForDestination = function (e, t, n) {
    n = n || Shopify.onError;
    var r = function () {
        jQuery.ajax("/cart/async_shipping_rates", {
            dataType: "json",
            success: function (n, i, o) {
                200 === o.status ? "function" == typeof t ? t(n.shipping_rates, e) : Shopify.onCartShippingRatesUpdate(n.shipping_rates, e) : setTimeout(r, 500)
            },
            error: n
        })
    };
    return r
}, Shopify.getCartShippingRatesForDestination = function (e, t, n) {
    n = n || Shopify.onError;
    var r = {
        type: "POST",
        url: "/cart/prepare_shipping_rates",
        data: Shopify.param({
            shipping_address: e
        }),
        success: Shopify.pollForCartShippingRatesForDestination(e, t, n),
        error: n
    };
    jQuery.ajax(r)
},
Shopify.getProduct = function (e, t) {
    jQuery.getJSON("/products/" + e + ".js", function (e, n) {
        "function" == typeof t ? t(e) : Shopify.onProduct(e)
    })
},
Shopify.changeItem = function (e, t, n) {
    var r = {
        type: "POST",
        url: "/cart/change.js",
        data: "quantity=" + t + "&id=" + e,
        dataType: "json",
        success: function (e) {
            "function" == typeof n ? n(e) : Shopify.onCartUpdate(e)
        },
        error: function (e, t) {
            Shopify.onError(e, t)
        }
    };
    jQuery.ajax(r)
}, Shopify.removeItem = function (e, t) {
    var n = {
        type: "POST",
        url: "/cart/change.js",
        data: "quantity=0&id=" + e,
        dataType: "json",
        success: function (e) {
            "function" == typeof t ? t(e) : Shopify.onCartUpdate(e)
        },
        error: function (e, t) {
            Shopify.onError(e, t)
        }
    };
    jQuery.ajax(n)
}, Shopify.clear = function (e) {
    var t = {
        type: "POST",
        url: "/cart/clear.js",
        data: "",
        dataType: "json",
        success: function (t) {
            "function" == typeof e ? e(t) : Shopify.onCartUpdate(t)
        },
        error: function (e, t) {
            Shopify.onError(e, t)
        }
    };
    jQuery.ajax(t)
}, Shopify.updateCartFromForm = function (e, t) {
    var n = {
        type: "POST",
        url: "/cart/update.js",
        data: jQuery("#" + e).serialize(),
        dataType: "json",
        success: function (e) {
            "function" == typeof t ? t(e) : Shopify.onCartUpdate(e)
        },
        error: function (e, t) {
            Shopify.onError(e, t)
        }
    };
    jQuery.ajax(n)
}, Shopify.updateCartAttributes = function (e, t) {
    var n = "";
    jQuery.isArray(e) ? jQuery.each(e, function (e, t) {
        var r = attributeToString(t.key);
        "" !== r && (n += "attributes[" + r + "]=" + attributeToString(t.value) + "&")
    }) : "object" == typeof e && null !== e && jQuery.each(e, function (e, t) {
        n += "attributes[" + attributeToString(e) + "]=" + attributeToString(t) + "&"
    });
    var r = {
        type: "POST",
        url: "/cart/update.js",
        data: n,
        dataType: "json",
        success: function (e) {
            "function" == typeof t ? t(e) : Shopify.onCartUpdate(e)
        },
        error: function (e, t) {
            Shopify.onError(e, t)
        }
    };
    jQuery.ajax(r)
}, Shopify.updateCartNote = function (e, t) {
    var n = {
        type: "POST",
        url: "/cart/update.js",
        data: "note=" + attributeToString(e),
        dataType: "json",
        success: function (e) {
            "function" == typeof t ? t(e) : Shopify.onCartUpdate(e)
        },
        error: function (e, t) {
            Shopify.onError(e, t)
        }
    };
    jQuery.ajax(n)
}, jQuery.fn.jquery >= "1.4" ? Shopify.param = jQuery.param : (Shopify.param = function (e) {
    var t = [],
        n = function (e, n) {
            n = jQuery.isFunction(n) ? n() : n, t[t.length] = encodeURIComponent(e) + "=" + encodeURIComponent(n)
        };
    if (jQuery.isArray(e) || e.jquery) jQuery.each(e, function () {
        n(this.name, this.value)
    });
    else
        for (var r in e) Shopify.buildParams(r, e[r], n);
    return t.join("&").replace(/%20/g, "+")
}, Shopify.buildParams = function (e, t, n) {
    jQuery.isArray(t) && t.length ? jQuery.each(t, function (t, r) {
        rbracket.test(e) ? n(e, r) : Shopify.buildParams(e + "[" + ("object" == typeof r || jQuery.isArray(r) ? t : "") + "]", r, n)
    }) : null != t && "object" == typeof t ? Shopify.isEmptyObject(t) ? n(e, "") : jQuery.each(t, function (t, r) {
        Shopify.buildParams(e + "[" + t + "]", r, n)
    }) : n(e, t)
}, Shopify.isEmptyObject = function (e) {
    for (var t in e) return !1;
    return !0
});