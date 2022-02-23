var clean = window.clean = window.clean || {};
(function () {
    clean.Tazkira = function (opt) {
        this.el = opt.el;
        this.sib = $('#' + this.el.attr('sibling'));
        this.sib.val = '';
        this.form = opt.form;
        this.row = opt.parent;
        this.template = "<div class='col-md-2 col-sm-12 col-xs-12 pull-right tazkira-group $g-class'><div class='form-group'><label class='text-bold'><span class='text-danger pull-left'>&nbsp;*</span>$Label</label><input id='$id' name='$name' class='form-control alt-tazkira' required/></div></div>";
        this.serialNo = {};
        this.juld = {};
        this.page = {};
        this.No = {};
        this.init();

    };
    clean.Tazkira.prototype = {
        init: function () {
            var self = this;

            var sn = self.template.replace('$Label', 'نمبر سند هویت').replace('$id', 'sn').replace('$g-class', 'always').replace('$name', 'SerialNumber');
            self.row.append(sn);
            self.serialNo = $('#sn');

            var jd = self.template.replace('$Label', 'جلد').replace('$id', 'jd').replace('$g-class', 'old').replace('$name', 'Juld');
            self.row.append(jd);
            self.juld = $('#jd');

            var pg = self.template.replace('$Label', 'نمبر صفحه').replace('$id', 'pg').replace('$g-class', 'old').replace('$name', 'Page');
            self.row.append(pg);
            self.page = $('#pg');

            var no = self.template.replace('$Label', 'نمبر ثبت').replace('$id', 'no').replace('$g-class', 'old').replace('$name', 'No');;
            self.row.append(no);
            self.No = $('#no');

            self.sib.change(function () {
                var v = $(this).find("option:selected").text().trim();
                self.sib.val = v;
                self.changeselect(v);
            });

            $('.old').hide();
        },
        changeselect: function (v) {
            var self = this;
            if (v == 'تذکره ورقی') {
                $('.old').show();
                $('.alt-tazkira').val("");
            }
            else {
                $('.old').hide();

            }

        },
        getExpr: function (col) {
            var v = "{S:'%" + (this.serialNo.val().trim() != "" ? this.serialNo.val().trim() : "") + "'," +
                "J:'%" + (this.juld.val().trim() != "" ? this.juld.val().trim() : "") + "'," +
                "P:'%" + (this.page.val().trim() != "" ? this.page.val().trim() : "") + "'," +
                "N:'%" + (this.No.val().trim() != "" ? this.No.val().trim() : "") + "'}";
            var expr = {};
            expr.fn = "endswith";
            expr.expr = {};
            expr.expr[col] = v;
            return expr;
        },
        validate: function () {
            if (this.sib.val == 'تذکره ورقی')
                return $('.alt-tazkira').valid();
            else
                return $('#sn').valid();
        },
        val: function (v) {
            if (v !== undefined) {
                v = v + "";
                // clear
                this.serialNo.val('');
                this.juld.val('');
                this.page.val('');
                this.No.val('');
                var v2;
                try {
                    eval('v2 = ' + v);
                    this.serialNo.val(v2.S.replace(',', ''));
                    this.juld.val(v2.J.replace(',', ''));
                    this.page.val(v2.P.replace(',', ''));
                    this.No.val(v2.N.replace(',', ''));
                }
                catch (e) {
                    v2 = v.split(" ");
                }
                // set
                return v;
            }

            var vals = [this.serialNo.val(), this.juld.val(), this.page.val(), this.No.val()];
            if (!this.serialNo.val().trim() && !this.juld.val().trim() && !this.page.val().trim() && !this.No.val().trim()) return '';
            // get
            v = clean.format("{S: '{serialNoID}', J:'{JuldID}',P:'{PageID}',N:'{NoID}'}", vals, { serialNoID: 0, JuldID: 1, PageID: 2, NoID: 3 });
            return v;
        }
    }
})();






