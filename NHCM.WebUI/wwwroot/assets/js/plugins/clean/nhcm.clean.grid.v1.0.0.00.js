var clean = window.clean = window.clean || {};
(function () {
    clean.grid = function (opt) {
        this.opt = opt = opt || {};
        this.el = opt.el;
        this.form = {};
        this.type = this.el.attr('type')
        this.init(this.el);
    }
    clean.page.prototype = {
        init: function (opt) {
            var self = this;
            self.construct();
        },

        construct: function () {

        }
    };
})();