After regeneration of help documentation replace script in Index.html with this one:

        window.location.replace"html/{GUID}.htm");

with this one:

        var base = window.location.href;
        base = base.substr(0, base.lastIndexOf("/") + 1);
        window.location.replace(base + "html/{GUID}.htm");
