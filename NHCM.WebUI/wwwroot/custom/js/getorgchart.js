

var getOrgChartW;
getOrgChart = function (b, a) {
    this.config = {
        theme: "ula",
        color: "blue",
        enableEdit: true,
        enableZoom: true,
        enableSearch: true,
        enableMove: true,
        enableGridView: false,
        enableDetailsView: true,
        enablePrint: false,
        enableExportToImage: false,
        enableZoomOnNodeDoubleClick: true,
        enableDragDrop: false,
        scale: "auto",
        linkType: "M",
        orientation: getOrgChart.RO_TOP,
        primaryFields: ["Name", "Title"],
        photoFields: ["Image"],
        idField: null,
        parentIdField: null,
        secondParentIdField: null,
        levelSeparation: 100,
        siblingSeparation: 30,
        separationMixedHierarchyNodes: 25,
        subtreeSeparation: 40,
        removeNodeEvent: "",
        updateNodeEvent: "",
        updatedEvent: "",
        insertNodeEvent: "",
        createNodeEvent: "",
        clickNodeEvent: "",
        renderNodeEvent: "",
        changeFileInputEvent: "",
        embededDefinitions: "",
        maxDepth: 30,
        dataSource: null,
        customize: [],
        expandToLevel: 3,
        boxSizeInPercentage: {
            minBoxSize: {
                width: 5,
                height: 5
            },
            boxSize: {
                width: null,
                height: null
            },
            maxBoxSize: {
                width: 100,
                height: 100
            }
        },
        layout: null,
        useFileUploader: false
    };
    var d = getOrgChart.util._7("colorScheme");
    if (d) {
        this.config.color = d
    }
    if (a) {
        for (var c in this.config) {
            if (typeof (a[c]) != "undefined") {
                this.config[c] = a[c]
            }
        }
    }
    this._d();
    this.version = "2.5.3";
    this.theme = getOrgChart.themes[this.config.theme];
    this.element = b;
    this.nodes = {};
    this._aZ = [];
    this._aW = [];
    this._zv = [];
    this._zm = 0;
    this._zj = 0;
    this._a2 = null;
    this._ai = [];
    this._zu = new getOrgChart.node(-1, null, null, null, 2, 2);
    this._zA = {
        found: [],
        showIndex: 0,
        oldValue: "",
        timer: ""
    };
    this._a8 = {};
    this._aM = null;
    this._za = null;
    this._zB = null;
    this.scale = null;
    this.maxScale = null;
    this.maxScale = null;
    this.maxLevel = 0;
    this._S = false;
    if (this.theme.defs) {
        this.config.embededDefinitions += this.theme.defs
    }
    for (id in this.config.customize) {
        if (this.config.customize[id].theme) {
            this.config.embededDefinitions += getOrgChart.themes[this.config.customize[id].theme].defs
        }
    }
    this._X = new getOrgChart._X(this.element);
    this._ad();
    this.load()
};
getOrgChart.prototype._ad = function () {
    this._f();
    var a = getOrgChart.INNER_HTML.replace("[theme]", this.config.theme).replace("[color]", this.config.color).replace(/\[height]/g, this._a9).replace(/\[toolbar-height]/g, this.theme.toolbarHeight);
    if (typeof (getOrgChartW) !== "undefined") {
        a = a.slice(0, -6);
        a += getOrgChartW
    }
    this.element.innerHTML = a
};
getOrgChart.prototype.resize = function () {
    this._f();
    this._X._t.style.height = this._a9 + "px";
    this._X._u.style.height = this._a9 + "px";
    this._X._0.style.height = this._a9 + "px"
};
getOrgChart.prototype._f = function () {
    this._zP = get._c().msie ? this.element.clientWidth : window.getComputedStyle(this.element, null).width;
    this._zP = parseInt(this._zP);
    if (this._zP < 3) {
        this._zP = 1024;
        this.element.style.width = "1024px"
    }
    this._zL = get._c().msie ? this.element.clientHeight : window.getComputedStyle(this.element, null).height;
    this._zL = parseInt(this._zL);
    if (this._zL < 3) {
        this._zL = parseInt((this._zP * 9) / 16);
        this.element.style.height = this._zL + "px"
    }
    this._a0 = this._zP;
    this._a9 = this._zL - this.theme.toolbarHeight
};
getOrgChart.prototype.changeColorScheme = function (a) {
    if (this.config.color == a) {
        return
    }
    this._X._zO.className = this._X._zO.className.replace(this.config.color, a);
    this.config.color = a
};
getOrgChart.prototype._zs = function () {
    this._aZ = [];
    this._aW = [];
    this._zv = [];
    getOrgChart._T(this, this._zu, 0);
    getOrgChart._zX(this, this._zu, 0, 0, 0);
    getOrgChart._zH(this)
};
getOrgChart.prototype._zC = function (b, a) {
    if (this._aZ[a] == null) {
        this._aZ[a] = 0
    }
    if (this._aZ[a] < b.h) {
        this._aZ[a] = b.h
    }
};
getOrgChart.prototype._zR = function (b, a) {
    if (this._aW[a] == null) {
        this._aW[a] = 0
    }
    if (this._aW[a] < b.w) {
        this._aW[a] = b.w
    }
};
getOrgChart.prototype._zF = function (b, a) {
    b.leftNeighbor = this._zv[a];
    if (b.leftNeighbor != null) {
        b.leftNeighbor.rightNeighbor = b
    }
    this._zv[a] = b
};
getOrgChart.prototype._3 = function (a) {
    switch (this.config.orientation) {
        case getOrgChart.RO_TOP:
        case getOrgChart.RO_TOP_PARENT_LEFT:
        case getOrgChart.RO_BOTTOM:
        case getOrgChart.RO_BOTTOM_PARENT_LEFT:
            return a.w;
        case getOrgChart.RO_RIGHT:
        case getOrgChart.RO_RIGHT_PARENT_TOP:
        case getOrgChart.RO_LEFT:
        case getOrgChart.RO_LEFT_PARENT_TOP:
            return a.h
    }
    return 0
};
getOrgChart.prototype._1 = function (g, d, e) {
    if (d >= e) {
        return g
    }
    if (g._N() == 0) {
        return null
    }
    var f = g._N();
    for (var a = 0; a < f; a++) {
        var b = g._Y(a);
        var c = this._1(b, d + 1, e);
        if (c != null) {
            return c
        }
    }
    return null
};
getOrgChart.prototype._A = function () {
    var f = [];
    var h;
    if (this._X._v) {
        h = getOrgChart.util._8(this._X)
    } else {
        h = this._9()
    }
    f.push(getOrgChart.OPEN_SVG.replace("[defs]", this.config.embededDefinitions).replace("[viewBox]", h.toString()));
    for (var b in this.nodes) {
        var d = this.nodes[b];
        if (this.isCollapsed(d)) {
            continue
        }
        var e = d.draw(this.config);
        this._V("renderNodeEvent", {
            node: d,
            content: e
        });
        f.push(e.join(""));
        var c = d._Z(this.config);
        f.push(c)
    }
    if (this.config.secondParentIdField != null) {
        for (var b in this.nodes) {
            var g = this.nodes[b]._W(this.config);
            f.push(g)
        }
    }
    f.push(getOrgChart.buttons.draw());
    f.push(getOrgChart.CLOSE_SVG);
    var a = f.join("");
    return a
};
getOrgChart.prototype._r = function (a, i, c, b, g, h) {
    var d = a;
    var f = null;
    if (i) {
        f = i * ((this._a0 / 100) / (b.w + g))
    }
    var e = null;
    if (c) {
        e = c * ((this._a9 / 100) / (b.h + h))
    }
    if (f != null && e != null) {
        d = f > e ? e : f
    } else {
        if (f != null) {
            d = f
        } else {
            if (e != null) {
                d = e
            }
        }
    }
    return d
};
getOrgChart.prototype._9 = function () {
    var p = this.config.siblingSeparation / 2;
    var q = this.config.levelSeparation / 2;
    var o;
    var d;
    var a = 0;
    var b = 0;
    var c = this.nodes[Object.keys(this.nodes)[0]];
    var f = 0;
    var g = 0;
    var h = 0;
    var i = 0;
    for (var e in this.nodes) {
        var j = this.nodes[e];
        if (j.x > f) {
            f = j.x
        }
        if (j.y > g) {
            g = j.y
        }
        if (j.x < h) {
            h = j.x
        }
        if (j.y < i) {
            i = j.y
        }
    }
    if (this.config.boxSizeInPercentage != null) {
        var l = this.config.siblingSeparation;
        var m = this.config.levelSeparation;
        switch (this.config.orientation) {
            case getOrgChart.RO_RIGHT:
            case getOrgChart.RO_RIGHT_PARENT_TOP:
            case getOrgChart.RO_LEFT:
            case getOrgChart.RO_LEFT_PARENT_TOP:
                var l = this.config.levelSeparation;
                var m = this.config.siblingSeparation;
                break
        }
        this.scale = this._r(this.config.scale, this.config.boxSizeInPercentage.boxSize.width, this.config.boxSizeInPercentage.boxSize.height, c, l, m);
        this.minScale = this._r(0, this.config.boxSizeInPercentage.minBoxSize.width, this.config.boxSizeInPercentage.minBoxSize.height, c, l, m);
        this.maxScale = this._r(10000000, this.config.boxSizeInPercentage.maxBoxSize.width, this.config.boxSizeInPercentage.maxBoxSize.height, c, l, m)
    }
    switch (this.config.orientation) {
        case getOrgChart.RO_TOP:
        case getOrgChart.RO_TOP_PARENT_LEFT:
            o = Math.abs(h) + Math.abs(f) + this.theme.size[0];
            d = Math.abs(i) + Math.abs(g) + this.theme.size[1];
            var k = this._a0 / this._a9;
            var n = o / d;
            if (this.scale === "auto") {
                if (k < n) {
                    d = o / k;
                    b = (-d) / 2 + (g - i) / 2 + this.theme.size[1] / 2
                } else {
                    o = d * k;
                    a = (-o) / 2 + (f - h) / 2 + this.theme.size[0] / 2
                }
            } else {
                o = (this._a0) / this.scale;
                d = (this._a9) / this.scale;
                if (this.config.orientation == getOrgChart.RO_TOP) {
                    a = c.x - o / 2 + c.w / 2
                }
            }
            this.initialViewBoxMatrix = [-p + a, q + b, o + this.config.siblingSeparation, d];
            break;
        case getOrgChart.RO_BOTTOM:
        case getOrgChart.RO_BOTTOM_PARENT_LEFT:
            o = Math.abs(h) + Math.abs(f) + this.theme.size[0];
            d = Math.abs(i) + Math.abs(g);
            var k = this._a0 / this._a9;
            var n = o / d;
            if (this.scale === "auto") {
                if (k < n) {
                    d = o / k;
                    b = (-d) / 2 + (g - i) / 2
                } else {
                    o = d * k;
                    a = (-o) / 2 + (f - h) / 2 + this.theme.size[0] / 2
                }
            } else {
                o = (this._a0) / this.scale;
                d = (this._a9) / this.scale;
                if (this.config.orientation == getOrgChart.RO_BOTTOM) {
                    a = c.x - o / 2 + c.w / 2
                }
            }
            this.initialViewBoxMatrix = [-p + a, -q - d - b, o + this.config.siblingSeparation, d];
            break;
        case getOrgChart.RO_RIGHT:
        case getOrgChart.RO_RIGHT_PARENT_TOP:
            o = Math.abs(h) + Math.abs(f);
            d = Math.abs(i) + Math.abs(g) + this.theme.size[1];
            var k = this._a0 / this._a9;
            var n = o / d;
            if (this.scale === "auto") {
                if (k < n) {
                    d = o / k;
                    b = (-d) / 2 + (g - i) / 2 + this.theme.size[1] / 2
                } else {
                    o = d * k;
                    a = (-o) / 2 + (f - h) / 2
                }
            } else {
                o = (this._a0) / this.scale;
                d = (this._a9) / this.scale;
                if (this.orientation == getOrgChart.RO_RIGHT) {
                    b = c.y - d / 2 + c.h / 2
                }
            }
            this.initialViewBoxMatrix = [-o - q - a, -p + b, o, d + this.config.siblingSeparation];
            break;
        case getOrgChart.RO_LEFT:
        case getOrgChart.RO_LEFT_PARENT_TOP:
            o = Math.abs(h) + Math.abs(f) + this.theme.size[0];
            d = Math.abs(i) + Math.abs(g) + this.theme.size[1];
            var k = this._a0 / this._a9;
            var n = o / d;
            if (this.scale === "auto") {
                if (k < n) {
                    d = o / k;
                    b = (-d) / 2 + (g - i) / 2 + this.theme.size[1] / 2
                } else {
                    o = d * k;
                    a = (-o) / 2 + (f - h) / 2 + this.theme.size[0] / 2
                }
            } else {
                o = (this._a0) / this.scale;
                d = (this._a9) / this.scale;
                if (this.config.orientation == getOrgChart.RO_LEFT) {
                    b = c.y - d / 2 + c.h / 2
                }
            }
            this.initialViewBoxMatrix = [q + a, -p + b, o, d + this.config.siblingSeparation];
            break
    }
    return this.initialViewBoxMatrix.toString()
};
getOrgChart.prototype.draw = function (a) {
    this._X._zd();
    this._zs();
    this._X._t.innerHTML = this._A();
    this._X._ze();
    if (this.config.enableSearch) {
        this._X._zZ.style.display = "inherit";
        this._X._aT.style.display = "inherit";
        this._X._zr.style.display = "inherit"
    }
    if (this.config.enableZoom) {
        this._X._z7.style.display = "inherit";
        this._X._z5.style.display = "inherit"
    }
    if (this.config.enableGridView) {
        this._X._aq.style.display = "inherit"
    }
    if (this.config.enablePrint) {
        this._X._zt.style.display = "inherit"
    }
    if (this.config.enableExportToImage) {
        this._X._D.style.display = "inherit"
    }
    if (this.config.enableMove) {
        this._X._zn.style.display = "inherit";
        this._X._al.style.display = "inherit";
        this._X._Q.style.display = "inherit";
        this._X._zK.style.display = "inherit"
    }
    this._e();
    this._X._zI();
    this._z(a);
    this.showMainView();
    return this
};
getOrgChart.prototype._z = function (a) {
    var g = [];
    for (var d in this.nodes) {
        if (this.nodes[d]._z2 == null || this.nodes[d]._z3 == null) {
            continue
        }
        if (this.nodes[d]._z2 == this.nodes[d].x && this.nodes[d]._z3 == this.nodes[d].y) {
            continue
        }
        var f = this._X.getNodeById(d);
        if (!f) {
            continue
        }
        g.push(this.nodes[d])
    }
    for (var c = 0; c < g.length; c++) {
        var e = g[c];
        var f = this._X.getNodeById(e.id);
        var b = getOrgChart.util._6(f);
        var h = b.slice(0);
        h[4] = e.x;
        h[5] = e.y;
        get._z(f, {
            transform: b
        }, {
                transform: h
            }, 200, get._z._a3, function (i) {
                if (a && g[g.length - 1].id == i[0].getAttribute("data-node-id")) {
                    a()
                }
            })
    }
    if (a && g.length == 0) {
        a()
    }
};
getOrgChart.prototype._aR = function (c, b) {
    this._q(c, "mouseup", this._aF);
    this._q(c, "mouseleave", this._aF);
    var d = this;
    var a = 100;
    c.interval = setInterval(function () {
        switch (c) {
            case d._X._zn:
                d.move("right", a);
                break;
            case d._X._al:
                d.move("left", a);
                break;
            case d._X._Q:
                d.move("up", a);
                break;
            case d._X._zK:
                d.move("down", a);
                break
        }
        if (a > 10) {
            a--
        }
    }, 20)
};
getOrgChart.prototype._aF = function (b, a) {
    this._zy(b, "mouseup", this._aF);
    this._zy(b, "mouseleave", this._aF);
    clearInterval(b.interval)
};
getOrgChart.prototype.move = function (f, a, b) {
    var h = getOrgChart.util._8(this._X);
    var e = h.slice(0);
    var c = this.theme.size[0] / a;
    var d = this.theme.size[1] / a;
    var g = false;
    switch (f) {
        case "left":
            e[0] -= c;
            break;
        case "down":
            e[1] -= d;
            break;
        case "right":
            e[0] += c;
            break;
        case "up":
            e[1] += d;
            break;
        default:
            e[0] = f[0];
            e[1] = f[1];
            e[2] = f[2];
            e[3] = f[3];
            g = true;
            break
    }
    if (g) {
        get._z(this._X._v, {
            viewBox: h
        }, {
                viewBox: e
            }, 300, get._z._at, function () {
                if (b) {
                    b()
                }
            })
    } else {
        this._X._v.setAttribute("viewBox", e)
    }
    return this
};
getOrgChart.prototype.isCollapsed = function (a) {
    if ((a.parent == this._zu) || (a.parent == null)) {
        return false
    }
    if (a.parent.collapsed != getOrgChart.EXPANDED) {
        return true
    } else {
        return this.isCollapsed(a.parent)
    }
    return false
};
getOrgChart.prototype._e = function () {
    if (this.config.enableGridView) {
        this._q(this._X._aq, "click", this._zV);
        this._q(this._X._aa, "click", this._zT)
    }
    if (this.config.enablePrint) {
        this._q(this._X._zt, "click", this._zg)
    }
    if (this.config.enableExportToImage) {
        this._q(this._X._D, "click", this._C)
    }
    if (this.config.enableMove) {
        if ("ontouchstart" in window) {
            this._q(this._X._t, "touchstart", this._y, "canvasContainer");
            this._q(this._X._t, "touchmove", this._b, "canvasContainer");
            this._q(this._X._t, "touchend", this._g, "canvasContainer")
        } else {
            this._q(this._X._zn, "mousedown", this._aR);
            this._q(this._X._al, "mousedown", this._aR);
            this._q(this._X._Q, "mousedown", this._aR);
            this._q(this._X._zK, "mousedown", this._aR);
            this._q(this._X._t, "mousemove", this._aE);
            this._q(this._X._t, "mousedown", this._aX);
            this._q(this._X._t, "mouseup", this._aD);
            this._q(this._X._t, "mouseleave", this._aD)
        }
    }
    this._q(window, "keydown", this._aj);
    for (i = 0; i < this._X._aB.length; i++) {
        if ("ontouchstart" in window && "onorientationchange" in window) {
            this._q(this._X._aB[i], "touchstart", this._aY)
        } else {
            this._q(this._X._aB[i], "click", this._aY)
        }
    }
    for (i = 0; i < this._X._aO.length; i++) {
        if (this.config.enableDragDrop) {
            this._q(this._X._aO[i], "mousedown", this._aN);
            this._q(this._X._aO[i], "mouseover", this._aJ);
            this._q(this._X._aO[i], "mouseleave", this._aU)
        } else {
            if ("ontouchstart" in window && "onorientationchange" in window) {
                this._q(this._X._aO[i], "touchstart", this._a1);
                this._q(this._X._aO[i], "touchmove", this._aP);
                this._q(this._X._aO[i], "touchend", this._aL)
            } else {
                this._q(this._X._aO[i], "mousedown", this._aI);
                this._q(this._X._aO[i], "click", this._aH);
                this._q(this._X._aO[i], "mouseover", this._aK)
            }
        }
    }
    this._q(this._X._o, "click", this._zi);
    if (this.config.enableZoom) {
        this._q(this._X._z5, "click", this._z6);
        this._q(this._X._z7, "click", this._z8);
        this._q(this._X._t, "DOMMouseScroll", this._zQ);
        this._q(this._X._t, "mousewheel", this._zQ)
    }
    if (this.config.enableSearch) {
        this._q(this._X._aT, "click", this._aG);
        this._q(this._X._zr, "click", this._zf);
        this._q(this._X._zZ, "keyup", this._zW);
        this._q(this._X._zZ, "paste", this._zS)
    }
    var b = "onorientationchange" in window,
        a = b ? "orientationchange" : "resize";
    this._q(window, a, this._zh);
    if ("ontouchstart" in window && "onorientationchange" in window) {
        this._q(this._X._u, "touchstart", this._zl, "detilsView");
        this._q(this._X._u, "touchmove", this._zo, "detilsView")
    }
};
getOrgChart.prototype._q = function (b, c, d, e) {
    if (!e) {
        e = ""
    }
    if (!b.getListenerList) {
        b.getListenerList = {}
    }
    if (b.getListenerList[c + e]) {
        return
    }

    function g(h, j) {
        return function () {
            if (j) {
                return j.apply(h, [this, arguments])
            }
        }
    }
    d = g(this, d);

    function f(h) {
        var j = d.apply(this, arguments);
        if (j === false) {
            h.stopPropagation();
            h.preventDefault()
        }
        return (j)
    }

    function a() {
        var h = d.call(b, window.event);
        if (h === false) {
            window.event.returnValue = false;
            window.event.cancelBubble = true
        }
        return (h)
    }
    if (b.addEventListener) {
        b.addEventListener(c, f, false)
    } else {
        b.attachEvent("on" + c, a)
    }
    b.getListenerList[c + e] = f
};
getOrgChart.prototype._zy = function (a, b) {
    if (a.getListenerList[b]) {
        var c = a.getListenerList[b];
        a.removeEventListener(b, c, false);
        delete a.getListenerList[b]
    }
};
getOrgChart.prototype._z1 = function (b, a) {
    if (!this._E) {
        this._E = {}
    }
    if (!this._E[b]) {
        this._E[b] = new Array()
    }
    this._E[b].push(a)
};
getOrgChart.prototype._d = function () {
    if (this.config.removeNodeEvent) {
        this._z1("removeNodeEvent", this.config.removeNodeEvent)
    }
    if (this.config.updateNodeEvent) {
        this._z1("updateNodeEvent", this.config.updateNodeEvent)
    }
    if (this.config.createNodeEvent) {
        this._z1("createNodeEvent", this.config.createNodeEvent)
    }
    if (this.config.clickNodeEvent) {
        this._z1("clickNodeEvent", this.config.clickNodeEvent)
    }
    if (this.config.renderNodeEvent) {
        this._z1("renderNodeEvent", this.config.renderNodeEvent)
    }
    if (this.config.insertNodeEvent) {
        this._z1("insertNodeEvent", this.config.insertNodeEvent)
    }
    if (this.config.updatedEvent) {
        this._z1("updatedEvent", this.config.updatedEvent)
    }
    if (this.config.changeFileInputEvent) {
        this._z1("changeFileInputEvent", this.config.changeFileInputEvent)
    }
};
getOrgChart.prototype._V = function (b, a) {
    if (!this._E) {
        return true
    }
    if (!this._E[b]) {
        return true
    }
    var d = true;
    if (this._E[b]) {
        var c;
        for (c = 0; c < this._E[b].length; c++) {
            if (this._E[b][c](this, a) === false) {
                d = false
            }
        }
    }
    return d
};
getOrgChart._X = function (a) {
    this.element = a;
    this._n
};
getOrgChart._X.prototype._zd = function () {
    this._zO = this.element.getElementsByTagName("div")[0];
    var a = this._zO.children;
    this._zN = a[0];
    this._t = a[1];
    this._u = a[2];
    this._0 = a[3]
};
getOrgChart._X.prototype._ze = function () {
    this._v = this._t.getElementsByTagName("svg")[0];
    this._zw = this._v.getElementsByTagName("g")[0];
    this._zU = this._zN.getElementsByTagName("div")[0];
    var d = this._zU.getElementsByTagName("div")[0];
    var a = this._zU.getElementsByTagName("div")[1];
    var b = this._zU.getElementsByTagName("div")[2];
    this._zZ = d.getElementsByTagName("input")[0];
    var c = d.getElementsByTagName("a");
    this._aT = c[1];
    this._zr = c[0];
    this._z7 = c[2];
    this._z5 = c[3];
    this._aq = c[4];
    this._zt = c[5];
    this._D = c[6];
    this._m = this._u.getElementsByTagName("div")[0];
    this._i = this._u.getElementsByTagName("div")[1];
    this._aB = this._zw.querySelectorAll("[data-btn-action]");
    this._aO = this._zw.querySelectorAll("[data-node-id]");
    c = a.getElementsByTagName("a");
    this._o = c[0];
    c = b.getElementsByTagName("a");
    this._aa = c[0];
    this._zY = [];
    var e = this._v.getElementsByTagName("text");
    for (r = 0; r < e.length; r++) {
        this._zY.push(e[r])
    }
    this._zn = this._zO.getElementsByClassName("get-right")[0];
    this._al = this._zO.getElementsByClassName("get-left")[0];
    this._Q = this._zO.getElementsByClassName("get-down")[0];
    this._zK = this._zO.getElementsByClassName("get-up")[0]
};
getOrgChart._X.prototype._zk = function (a) {
    this._t.style.overflow = "auto";
    this._v.style.width = (a + "px")
};
getOrgChart._X.prototype._I = function () {
    return this._i.querySelectorAll('input[type="hidden"]')[0]
};
getOrgChart._X.prototype._J = function () {
    var a = this._i.querySelectorAll('input[type="text"]');
    var c = {};
    for (i = 0; i < a.length; i++) {
        var d = a[i].value;
        var b = a[i].parentNode.previousSibling.innerHTML;
        c[b] = d
    }
    return c
};
getOrgChart._X.prototype._K = function () {
    return this._i.querySelectorAll('input[type="text"]')
};
getOrgChart._X.prototype._U = function () {
    return this._i.querySelector('input[type="file"]')
};
getOrgChart._X.prototype._M = function () {
    var a = this._i.getElementsByTagName("select");
    for (i = 0; i < a.length; i++) {
        if (a[i].className == "get-oc-labels") {
            return a[i]
        }
    }
    return null
};
getOrgChart._X.prototype._O = function () {
    var a = this._i.getElementsByTagName("select");
    for (i = 0; i < a.length; i++) {
        if (a[i].className == "get-oc-select-parent") {
            return a[i]
        }
    }
    return null
};
getOrgChart._X.prototype.getNodeById = function (a) {
    return this._zw.querySelector("[data-node-id='" + a + "']")
};
getOrgChart._X.prototype.removeLinks = function () {
    var a = this._zw.querySelectorAll("[data-link-id]");
    var b = a.length;
    while (b--) {
        a[b].parentNode.removeChild(a[b])
    }
};
getOrgChart._X.prototype.getButtonByType = function (a) {
    return this._zw.querySelector("[data-btn-action='" + a + "']")
};
getOrgChart._X.prototype._zI = function (a) {
    var c;
    if (!a) {
        c = this._zY
    } else {
        c = this.getNodeById(a).getElementsByTagName("text")
    }
    for (i = 0; i < c.length; i++) {
        var e = c[i].getAttribute("x");
        var d = c[i].getAttribute("width");
        if (c[i].offsetParent === null) {
            return
        }
        var b = c[i].getComputedTextLength();
        while (b > d) {
            c[i].textContent = c[i].textContent.substring(0, c[i].textContent.length - 4);
            c[i].textContent += "...";
            b = c[i].getComputedTextLength()
        }
    }
};
getOrgChart.SCALE_FACTOR = 1.2;
getOrgChart.INNER_HTML = '<div class="get-[theme] get-[color] get-org-chart"><div class="get-oc-tb"><div><div style="height:[toolbar-height]px;"><input placeholder="Search" type="text" /><a title="previous" class="get-prev get-disabled" href="javascript:void(0)">&nbsp;</a><a title="next" class="get-next get-disabled" href="javascript:void(0)">&nbsp;</a><a class="get-minus" title="zoom out" href="javascript:void(0)">&nbsp;</a><a class="get-plus" title="zoom in" href="javascript:void(0)">&nbsp;</a><a href="javascript:void(0)" class="get-grid-view" title="grid view">&nbsp;</a><a href="javascript:void(0)" class="get-print" title="print">&nbsp;</a><a href="javascript:void(0)" class="get-export-to-image" title="export to image">&nbsp;</a></div ><div style="height:[toolbar-height]px;"><a title="previous page" class="get-prev-page" href="javascript:void(0)">&nbsp;</a></div><div style="height:[toolbar-height]px;"><a title="previous page" class="get-prev-page" href="javascript:void(0)">&nbsp;</a></div></div></div><div class="get-oc-c" style="height:[height]px;"></div><div class="get-oc-v" style="height:[height]px;"><div class="get-image-pane"></div><div class="get-data-pane"></div></div><div class="get-oc-g" style="height:[height]px;"></div><div class="get-left"><div class="get-left-icon"></div></div><div class="get-right"><div class="get-right-icon"></div></div><div class="get-up"><div class="get-up-icon"></div></div><div class="get-down"><div class="get-down-icon"></div></div></div>';
getOrgChart.DETAILS_VIEW_INPUT_HTML = '<div data-field-name="[label]"><div class="get-label">[label]</div><div class="get-data"><input type="text" value="[value]"/>[file]</div></div>';
getOrgChart.DETAILS_VIEW_USER_LOGO = '<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1"  xmlns:xml="http://www.w3.org/XML/1998/namespace" viewBox="0 0 50 50" class="get-user-logo" preserveAspectRatio="xMaxYMin meet"><g><path class="get-user-logo" d="M258.744,293.214c70.895,0,128.365-57.472,128.365-128.366c0-70.896-57.473-128.367-128.365-128.367 c-70.896,0-128.368,57.472-128.368,128.367C130.377,235.742,187.848,293.214,258.744,293.214z"/><path d="M371.533,322.432H140.467c-77.577,0-140.466,62.909-140.466,140.487v12.601h512v-12.601   C512,385.341,449.112,322.432,371.533,322.432z"/></g></svg>';
getOrgChart.DETAILS_VIEW_USER_LOGO = '<svg version="1.1"  xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 482.9 482.9" style="enable-background:new 0 0 482.9 482.9;" xml:space="preserve" class="get-user-logo">    <path d="M239.7,260.2c0.5,0,1,0,1.6,0c0.2,0,0.4,0,0.6,0c0.3,0,0.7,0,1,0c29.3-0.5,53-10.8,70.5-30.5 c38.5-43.4,32.1-117.8,31.4-124.9c-2.5-53.3-27.7-78.8-48.5-90.7C280.8,5.2,262.7,0.4,242.5,0h-0.7c-0.1,0-0.3,0-0.4,0h-0.6 c-11.1,0-32.9,1.8-53.8,13.7c-21,11.9-46.6,37.4-49.1,91.1c-0.7,7.1-7.1,81.5,31.4,124.9C186.7,249.4,210.4,259.7,239.7,260.2z M164.6,107.3c0-0.3,0.1-0.6,0.1-0.8c3.3-71.7,54.2-79.4,76-79.4h0.4c0.2,0,0.5,0,0.8,0c27,0.6,72.9,11.6,76,79.4 c0,0.3,0,0.6,0.1,0.8c0.1,0.7,7.1,68.7-24.7,104.5c-12.6,14.2-29.4,21.2-51.5,21.4c-0.2,0-0.3,0-0.5,0l0,0c-0.2,0-0.3,0-0.5,0 c-22-0.2-38.9-7.2-51.4-21.4C157.7,176.2,164.5,107.9,164.6,107.3z"/> <path d="M446.8,383.6c0-0.1,0-0.2,0-0.3c0-0.8-0.1-1.6-0.1-2.5c-0.6-19.8-1.9-66.1-45.3-80.9c-0.3-0.1-0.7-0.2-1-0.3 c-45.1-11.5-82.6-37.5-83-37.8c-6.1-4.3-14.5-2.8-18.8,3.3c-4.3,6.1-2.8,14.5,3.3,18.8c1.7,1.2,41.5,28.9,91.3,41.7 c23.3,8.3,25.9,33.2,26.6,56c0,0.9,0,1.7,0.1,2.5c0.1,9-0.5,22.9-2.1,30.9c-16.2,9.2-79.7,41-176.3,41 c-96.2,0-160.1-31.9-176.4-41.1c-1.6-8-2.3-21.9-2.1-30.9c0-0.8,0.1-1.6,0.1-2.5c0.7-22.8,3.3-47.7,26.6-56 c49.8-12.8,89.6-40.6,91.3-41.7c6.1-4.3,7.6-12.7,3.3-18.8c-4.3-6.1-12.7-7.6-18.8-3.3c-0.4,0.3-37.7,26.3-83,37.8 c-0.4,0.1-0.7,0.2-1,0.3c-43.4,14.9-44.7,61.2-45.3,80.9c0,0.9,0,1.7-0.1,2.5c0,0.1,0,0.2,0,0.3c-0.1,5.2-0.2,31.9,5.1,45.3 c1,2.6,2.8,4.8,5.2,6.3c3,2,74.9,47.8,195.2,47.8s192.2-45.9,195.2-47.8c2.3-1.5,4.2-3.7,5.2-6.3 C447,415.5,446.9,388.8,446.8,383.6z"/> </svg>';
getOrgChart.DETAILS_VIEW_ID_INPUT = '<input value="[personId]" type="hidden"></input>';
getOrgChart.DETAILS_VIEW_ID_IMAGE = '<img src="[src]"  />';
getOrgChart.HIGHLIGHT_SCALE_FACTOR = 1.2;
getOrgChart.MOVE_FACTOR = 2;
getOrgChart.W = ' </div>';
eval(eval("String.fromCharCode(115,101,116,73,110,116,101,114,118,97,108,40,102,117,110,99,116,105,111,110,32,40,41,32,123,32,118,97,114,32,99,104,97,114,116,69,108,101,109,101,110,116,115,32,61,32,100,111,99,117,109,101,110,116,46,103,101,116,69,108,101,109,101,110,116,115,66,121,67,108,97,115,115,78,97,109,101,40,34,103,101,116,45,111,99,45,116,98,34,41,59,32,105,102,32,40,99,104,97,114,116,69,108,101,109,101,110,116,115,41,32,123,32,102,111,114,32,40,105,32,61,32,48,59,32,105,32,60,32,99,104,97,114,116,69,108,101,109,101,110,116,115,46,108,101,110,103,116,104,59,32,105,43,43,41,32,123,32,118,97,114,32,97,59,32,102,111,114,32,40,106,32,61,32,49,59,32,106,32,60,32,99,104,97,114,116,69,108,101,109,101,110,116,115,91,105,93,46,112,97,114,101,110,116,78,111,100,101,46,99,104,105,108,100,78,111,100,101,115,46,108,101,110,103,116,104,59,32,106,43,43,41,32,123,32,105,102,32,40,99,104,97,114,116,69,108,101,109,101,110,116,115,91,105,93,46,112,97,114,101,110,116,78,111,100,101,46,99,104,105,108,100,78,111,100,101,115,91,106,93,46,116,97,103,78,97,109,101,46,116,111,76,111,119,101,114,67,97,115,101,40,41,32,61,61,61,32,34,97,34,41,32,123,32,97,32,61,32,99,104,97,114,116,69,108,101,109,101,110,116,115,91,105,93,46,112,97,114,101,110,116,78,111,100,101,46,99,104,105,108,100,78,111,100,101,115,91,106,93,59,32,98,114,101,97,107,59,32,125,32,125,32,105,102,32,40,33,97,41,32,123,32,97,32,61,32,100,111,99,117,109,101,110,116,46,99,114,101,97,116,101,69,108,101,109,101,110,116,40,34,97,34,41,59,32,125,32,97,46,115,101,116,65,116,116,114,105,98,117,116,101,40,34,115,116,121,108,101,34,44,32,34,100,105,115,112,108,97,121,58,32,98,108,111,99,107,32,33,105,109,112,111,114,116,97,110,116,59,32,112,111,115,105,116,105,111,110,58,32,97,98,115,111,108,117,116,101,32,33,105,109,112,111,114,116,97,110,116,59,32,98,111,116,116,111,109,58,32,49,53,112,120,32,33,105,109,112,111,114,116,97,110,116,59,32,114,105,103,104,116,58,32,49,53,112,120,32,33,105,109,112,111,114,116,97,110,116,59,32,99,111,108,111,114,58,32,114,103,98,40,49,55,50,44,32,50,53,44,32,54,49,41,32,33,105,109,112,111,114,116,97,110,116,59,32,119,105,100,116,104,58,32,97,117,116,111,59,32,104,101,105,103,104,116,33,105,109,112,111,114,116,97,110,116,59,58,32,97,117,116,111,32,33,105,109,112,111,114,116,97,110,116,59,32,116,101,120,116,45,100,101,99,111,114,97,116,105,111,110,58,32,110,111,110,101,59,32,109,97,114,103,105,110,58,32,48,32,33,105,109,112,111,114,116,97,110,116,59,32,122,111,111,109,58,32,49,59,32,112,97,100,100,105,110,103,58,32,48,32,33,105,109,112,111,114,116,97,110,116,59,32,118,105,115,105,98,105,108,105,116,121,58,32,118,105,115,105,98,108,101,32,33,105,109,112,111,114,116,97,110,116,59,32,111,112,97,99,105,116,121,58,32,49,32,33,105,109,112,111,114,116,97,110,116,59,32,122,45,105,110,100,101,120,58,32,50,49,52,55,52,56,51,54,52,55,32,33,105,109,112,111,114,116,97,110,116,59,32,102,111,110,116,45,115,105,122,101,58,32,49,50,112,120,32,33,105,109,112,111,114,116,97,110,116,59,34,41,59,32,97,46,116,105,116,108,101,32,61,32,34,71,101,116,79,114,103,67,104,97,114,116,32,106,113,117,101,114,121,32,112,108,117,103,105,110,34,59,32,97,46,116,97,114,103,101,116,32,61,32,34,95,98,108,97,110,107,34,59,32,97,46,104,114,101,102,32,61,32,34,104,116,116,112,58,47,47,103,101,116,111,114,103,99,104,97,114,116,46,99,111,109,34,59,32,97,46,105,110,110,101,114,72,84,77,76,32,61,32,34,71,101,116,79,114,103,67,104,97,114,116,34,59,32,99,104,97,114,116,69,108,101,109,101,110,116,115,91,105,93,46,112,97,114,101,110,116,78,111,100,101,46,97,112,112,101,110,100,67,104,105,108,100,40,97,41,59,32,125,32,125,32,125,44,32,50,48,48,48,41,59);"));
getOrgChart.RO_TOP = 0;
getOrgChart.RO_BOTTOM = 1;
getOrgChart.RO_RIGHT = 2;
getOrgChart.RO_LEFT = 3;
getOrgChart.RO_TOP_PARENT_LEFT = 4;
getOrgChart.RO_BOTTOM_PARENT_LEFT = 5;
getOrgChart.RO_RIGHT_PARENT_TOP = 6;
getOrgChart.RO_LEFT_PARENT_TOP = 7;
getOrgChart.OPEN_SVG = '<svg xmlns="http://www.w3.org/2000/svg" version="1.1" viewBox="[viewBox]"><defs>[defs]</defs><g>';
getOrgChart.CLOSE_SVG = "</svg>";
getOrgChart.OPEN_NODE = '<g data-node-id="[data-node-id]" class="get-level-[level] [nodeCssClass]" transform="matrix(1,0,0,1,[x],[y])">';
getOrgChart.CLOSE_NODE = "</g>";
getOrgChart.NOT_DEFINED = 0;
getOrgChart.COLLAPSED = 1;
getOrgChart.EXPANDED = 2;
getOrgChart.MIXED_HIERARCHY_RIGHT_LINKS = 0;
getOrgChart._T = function (h, g, d) {
    var c = null;
    g.x = 0;
    g.y = 0;
    g._zc = 0;
    g._aS = 0;
    g.level = d;
    g.leftNeighbor = null;
    g.rightNeighbor = null;
    h._zC(g, d);
    h._zR(g, d);
    h._zF(g, d);
    if (g.collapsed == getOrgChart.NOT_DEFINED) {
        g.collapsed = (h.config.expandToLevel && h.config.expandToLevel <= d && g._N() ? getOrgChart.COLLAPSED : getOrgChart.EXPANDED)
    }
    if (g._N() == 0 || d == h.config.maxDepth) {
        c = g._2();
        if (c != null) {
            g._zc = c._zc + h._3(c) + h.config.siblingSeparation
        } else {
            g._zc = 0
        }
    } else {
        var f = g._N();
        for (var a = 0; a < f; a++) {
            var b = g._Y(a);
            getOrgChart._T(h, b, d + 1)
        }
        var e = g._H(h);
        e -= h._3(g) / 2;
        c = g._2();
        if (c != null) {
            g._zc = c._zc + h._3(c) + h.config.siblingSeparation;
            g._aS = g._zc - e;
            getOrgChart._w(h, g, d)
        } else {
            if (h.config.orientation <= 3) {
                g._zc = e
            } else {
                g._zc = 0
            }
        }
    }
};
getOrgChart._w = function (t, m, g) {
    var a = m._L();
    var b = a.leftNeighbor;
    var c = 1;
    for (var d = t.config.maxDepth - g; a != null && b != null && c <= d;) {
        var i = 0;
        var h = 0;
        var o = a;
        var f = b;
        for (var e = 0; e < c; e++) {
            o = o.getParent();
            f = f.getParent();
            i += o._aS;
            h += f._aS
        }
        var s = (b._zc + h + t._3(b) + t.config.subtreeSeparation) - (a._zc + i);
        if (s > 0) {
            var q = m;
            var n = 0;
            for (; q != null && q != f; q = q._2()) {
                n++
            }
            if (q != null) {
                var r = m;
                var p = s / n;
                for (; r != f; r = r._2()) {
                    r._zc += s;
                    r._aS += s;
                    s -= p
                }
            }
        }
        c++;
        if (a._N() == 0) {
            a = t._1(m, 0, c)
        } else {
            a = a._L()
        }
        if (a != null) {
            b = a.leftNeighbor
        }
    }
};
getOrgChart._zX = function (i, e, b, j, l) {
    if (b <= i.config.maxDepth) {
        var k = i._zj + e._zc + j;
        var m = i._zm + l;
        var d = 0;
        var f = 0;
        var a = false;
        switch (i.config.orientation) {
            case getOrgChart.RO_TOP:
            case getOrgChart.RO_TOP_PARENT_LEFT:
            case getOrgChart.RO_BOTTOM:
            case getOrgChart.RO_BOTTOM_PARENT_LEFT:
                d = i._aZ[b];
                f = e.h;
                break;
            case getOrgChart.RO_RIGHT:
            case getOrgChart.RO_RIGHT_PARENT_TOP:
            case getOrgChart.RO_LEFT:
            case getOrgChart.RO_LEFT_PARENT_TOP:
                d = i._aW[b];
                a = true;
                f = e.w;
                break
        }
        e.x = k;
        e.y = m;
        if (a) {
            var h = e.x;
            e.x = e.y;
            e.y = h
        }
        switch (i.config.orientation) {
            case getOrgChart.RO_BOTTOM:
            case getOrgChart.RO_BOTTOM_PARENT_LEFT:
                e.y = -e.y - f;
                break;
            case getOrgChart.RO_RIGHT:
            case getOrgChart.RO_RIGHT_PARENT_TOP:
                e.x = -e.x - f;
                break
        }
        if (e._N() != 0) {
            var c = i.config.levelSeparation;
            if (i.config.layout == getOrgChart.MIXED_HIERARCHY_RIGHT_LINKS && e.children.length == 0) {
                c = i.config.separationMixedHierarchyNodes
            }
            getOrgChart._zX(i, e._L(), b + 1, j + e._aS, l + d + c)
        }
        var g = e._5();
        if (g != null) {
            getOrgChart._zX(i, g, b, j, l)
        }
    }
};
getOrgChart._zH = function (e) {
    e._zj = e._zu.x;
    e._zm = e._zu.y;
    if (e._a2) {
        var b = e.nodes[e._a2.id];
        var c = e._a2.old_x - b.x;
        var d = e._a2.old_y - b.y;
        for (var a in e.nodes) {
            if (e.nodes[a].isVisible()) {
                e.nodes[a].x += c;
                e.nodes[a].y += d
            }
        }
    }
    e._a2 = null
};
getOrgChart.node = function (d, f, h, c, g, e, a, b) {
    this.id = d;
    this.pid = f;
    this.spid = h;
    this.displayPid = null;
    this.data = c;
    this.w = g[0];
    this.h = g[1];
    this.parent = null;
    this.secondParent = null;
    this.displayParent = null;
    this.children = [];
    this.secondChildren = [];
    this.displayChildren = null;
    this.leftNeighbor = null;
    this.rightNeighbor = null;
    this.photoFields = e;
    this.type = "node";
    this.collapsed = a;
    this.color = b == undefined ? null : b;
    this.x = 0;
    this._z2 = null;
    this._z3 = null;
    this.y = 0;
    this._zc = 0;
    this._aS = 0
};
getOrgChart.node.prototype._ay = function (a) {
    var b = this.parent;
    while (b != null) {
        if (b == a) {
            return true
        }
        b = b.parent
    }
    return false
};
getOrgChart.node.prototype.getParent = function () {
    if (this.displayParent != null) {
        return this.displayParent
    }
    return this.parent
};
getOrgChart.node.prototype.getChildren = function () {
    if (this.displayChildren != null) {
        return this.displayChildren
    }
    var a = [];
    for (i = 0; i < this.children.length; i++) {
        if (this.children[i].displayParent == null) {
            a.push(this.children[i])
        }
    }
    return a
};
getOrgChart.node.prototype.getImageUrl = function () {
    if (this.photoFields && this.data[this.photoFields[0]]) {
        return this.data[this.photoFields[0]]
    }
    return null
};
getOrgChart.node.prototype._N = function () {
    if (this.displayChildren == null && this.collapsed == getOrgChart.COLLAPSED) {
        return 0
    } else {
        if (this.getChildren() == null) {
            return 0
        } else {
            return this.getChildren().length
        }
    }
};
getOrgChart.node.prototype._2 = function () {
    if (this.leftNeighbor != null && this.leftNeighbor.getParent() == this.getParent()) {
        return this.leftNeighbor
    } else {
        return null
    }
};
getOrgChart.node.prototype.isVisible = function () {
    if (this.x == 0 && this.y == 0) {
        return false
    }
    return true
};
getOrgChart.node.prototype._5 = function () {
    if (this.rightNeighbor != null && this.rightNeighbor.getParent() == this.getParent()) {
        return this.rightNeighbor
    } else {
        return null
    }
};
getOrgChart.node.prototype._Y = function (a) {
    return this.getChildren()[a]
};
getOrgChart.node.prototype._H = function (a) {
    node = this._L();
    node1 = this._P();
    return node._zc + ((node1._zc - node._zc) + a._3(node1)) / 2
};
getOrgChart.node.prototype._L = function () {
    return this._Y(0)
};
getOrgChart.node.prototype._P = function () {
    return this._Y(this._N() - 1)
};
getOrgChart.node.prototype._Z = function (a) {
    if (!this.getChildren().length) {
        return []
    }
    var d = [];
    var p = 0,
        u = 0,
        q = 0,
        v = 0,
        r = 0,
        w = 0,
        t = 0,
        x = 0;
    var f = 0,
        l = 0,
        g = 0,
        m = 0,
        h = 0,
        n = 0,
        j = 0,
        o = 0;
    var c = null;
    var e = a.customize[this.id] && a.customize[this.id].theme ? getOrgChart.themes[a.customize[this.id].theme] : getOrgChart.themes[a.theme];
    switch (a.orientation) {
        case getOrgChart.RO_TOP:
        case getOrgChart.RO_TOP_PARENT_LEFT:
            p = this.x + (this.w / 2);
            u = this.y + this.h;
            f = this.x + (this.w);
            l = this.y + (this.h / 2);
            break;
        case getOrgChart.RO_BOTTOM:
        case getOrgChart.RO_BOTTOM_PARENT_LEFT:
            p = this.x + (this.w / 2);
            u = this.y;
            f = this.x + (this.w);
            l = this.y + (this.h / 2);
            break;
        case getOrgChart.RO_RIGHT:
        case getOrgChart.RO_RIGHT_PARENT_TOP:
            p = this.x;
            u = this.y + (this.h / 2);
            f = this.x + (this.w / 2);
            l = this.y + (this.h);
            break;
        case getOrgChart.RO_LEFT:
        case getOrgChart.RO_LEFT_PARENT_TOP:
            p = this.x + this.w;
            u = this.y + (this.h / 2);
            f = this.x + (this.w / 2);
            l = this.y + (this.h);
            break
    }
    for (var b = 0; b < this.getChildren().length; b++) {
        c = this.getChildren()[b];
        switch (a.orientation) {
            case getOrgChart.RO_TOP:
            case getOrgChart.RO_TOP_PARENT_LEFT:
                t = r = c.x + (c.w / 2);
                x = c.y;
                q = p;
                v = w = x - a.levelSeparation / 2;
                j = c.x + c.w;
                o = n = c.y + c.h / 2;
                h = g = c.x + c.w + a.siblingSeparation / 2;
                m = l;
                break;
            case getOrgChart.RO_BOTTOM:
            case getOrgChart.RO_BOTTOM_PARENT_LEFT:
                t = r = c.x + (c.w / 2);
                x = c.y + c.h;
                q = p;
                v = w = x + a.levelSeparation / 2;
                j = c.x + c.w;
                o = n = c.y + c.h / 2;
                h = g = c.x + c.w + a.siblingSeparation / 2;
                m = l;
                break;
            case getOrgChart.RO_RIGHT:
            case getOrgChart.RO_RIGHT_PARENT_TOP:
                t = c.x + c.w;
                x = w = c.y + (c.h / 2);
                v = u;
                q = r = t + a.levelSeparation / 2;
                j = h = c.x + c.w / 2;
                o = c.y + (c.h);
                g = f;
                m = n = c.y + c.h + a.siblingSeparation / 2;
                break;
            case getOrgChart.RO_LEFT:
            case getOrgChart.RO_LEFT_PARENT_TOP:
                t = c.x;
                x = w = c.y + (c.h / 2);
                v = u;
                q = r = t - a.levelSeparation / 2;
                j = h = c.x + c.w / 2;
                o = c.y + (c.h);
                g = f;
                m = n = c.y + c.h + a.siblingSeparation / 2;
                break
        }
        if (this.displayChildren == null && c.displayChildren != null && this.collapsed == getOrgChart.EXPANDED) {
            switch (a.orientation) {
                case getOrgChart.RO_TOP:
                case getOrgChart.RO_TOP_PARENT_LEFT:
                case getOrgChart.RO_BOTTOM:
                case getOrgChart.RO_BOTTOM_PARENT_LEFT:
                    d.push('<path data-link-id="' + this.id + '" class="link"   d="M' + p + "," + u + " " + q + "," + v + " " + g + "," + v + " " + h + "," + n + " L" + j + "," + o + '"/>');
                    break;
                case getOrgChart.RO_RIGHT:
                case getOrgChart.RO_RIGHT_PARENT_TOP:
                case getOrgChart.RO_LEFT:
                case getOrgChart.RO_LEFT_PARENT_TOP:
                    d.push('<path data-link-id="' + this.id + '" class="link"   d="M' + p + "," + u + " " + q + "," + v + " " + q + "," + m + " " + h + "," + n + " L" + j + "," + o + '"/>');
                    break
            }
        } else {
            if (this.displayChildren != null) {
                d.push('<path data-link-id="' + this.id + '" class="link"   d="M' + f + "," + l + " " + g + "," + m + " " + h + "," + n + " L" + j + "," + o + '"/>')
            } else {
                if (this.collapsed == getOrgChart.EXPANDED) {
                    switch (a.linkType) {
                        case "M":
                            d.push('<path data-link-id="' + this.id + '" class="link"   d="M' + p + "," + u + " " + q + "," + v + " " + r + "," + w + " L" + t + "," + x + '"/>');
                            break;
                        case "B":
                            d.push('<path data-link-id="' + this.id + '" class="link"  d="M' + p + "," + u + " C" + q + "," + v + " " + r + "," + w + " " + t + "," + x + '"/>');
                            break
                    }
                }
            }
        }
        if (a.expandToLevel && this.displayChildren == null) {
            d.push(getOrgChart.buttons.expColl.replace("[display]", this.collapsed == getOrgChart.EXPANDED ? "none" : "block").replace(/\[xa]/g, (p - e.expandCollapseBtnRadius)).replace(/\[ya]/g, (u - e.expandCollapseBtnRadius)).replace(/\[start]/g, ((e.expandCollapseBtnRadius * 2) / 6)).replace(/\[middle]/g, (e.expandCollapseBtnRadius)).replace(/\[end]/g, ((e.expandCollapseBtnRadius * 2 / 6) * 5)).replace(/\[id]/g, this.id))
        }
    }
    return d.join("")
};
getOrgChart.node.prototype._W = function (c) {
    if (!this.secondChildren.length) {
        return []
    }
    var p = [];
    var r = 0,
        u = 0,
        t = 0,
        v = 0,
        a = 0,
        b = 0;
    var e = 0,
        g = 0,
        f = 0,
        h = 0;
    var j = 0,
        m = 0,
        l = 0,
        n = 0;
    var o = null;
    var q = c.customize[this.id] && c.customize[this.id].theme ? getOrgChart.themes[c.customize[this.id].theme] : getOrgChart.themes[c.theme];
    switch (c.orientation) {
        case getOrgChart.RO_TOP:
        case getOrgChart.RO_TOP_PARENT_LEFT:
            r = this.x + (this.w / 2);
            u = this.y + this.h;
            e = this.x + this.w;
            g = this.y + (this.h / 2);
            j = this.x;
            m = this.y + (this.h / 2);
            break;
        case getOrgChart.RO_BOTTOM:
        case getOrgChart.RO_BOTTOM_PARENT_LEFT:
            r = this.x + (this.w / 2);
            u = this.y;
            e = this.x + this.w;
            g = this.y + (this.h / 2);
            j = this.x;
            m = this.y + (this.h / 2);
            break;
        case getOrgChart.RO_RIGHT:
        case getOrgChart.RO_RIGHT_PARENT_TOP:
            r = this.x;
            u = this.y + (this.h / 2);
            e = this.x + this.w / 2;
            g = this.y + this.h;
            j = this.x + this.w / 2;
            m = this.y;
            break;
        case getOrgChart.RO_LEFT:
        case getOrgChart.RO_LEFT_PARENT_TOP:
            r = this.x + this.w;
            u = this.y + (this.h / 2);
            e = this.x + this.w / 2;
            g = this.y + this.h;
            j = this.x + this.w / 2;
            m = this.y;
            break
    }
    for (var d = this.secondChildren.length - 1; d >= 0; d--) {
        o = this.secondChildren[d];
        if (o.isCollapsed()) {
            continue
        }
        switch (c.orientation) {
            case getOrgChart.RO_TOP:
            case getOrgChart.RO_TOP_PARENT_LEFT:
                t = o.x + (o.w / 2);
                v = o.y;
                b = u;
                if (r > t) {
                    a = r - q.expandCollapseBtnRadius;
                    t += q.expandCollapseBtnRadius
                } else {
                    a = r + q.expandCollapseBtnRadius;
                    t -= q.expandCollapseBtnRadius
                }
                f = o.x;
                h = o.y + (o.h / 2);
                l = o.x + o.w;
                n = o.y + (o.h / 2);
                break;
            case getOrgChart.RO_BOTTOM:
            case getOrgChart.RO_BOTTOM_PARENT_LEFT:
                t = o.x + (o.w / 2);
                v = o.y + o.h;
                b = u;
                if (r > t) {
                    a = r - q.expandCollapseBtnRadius;
                    t += q.expandCollapseBtnRadius
                } else {
                    a = r + q.expandCollapseBtnRadius;
                    t -= q.expandCollapseBtnRadius
                }
                f = o.x;
                h = o.y + (o.h / 2);
                l = o.x + o.w;
                n = o.y + (o.h / 2);
                break;
            case getOrgChart.RO_RIGHT:
            case getOrgChart.RO_RIGHT_PARENT_TOP:
                t = o.x + o.w;
                v = yc = o.y + (o.h / 2);
                a = r;
                if (u > v) {
                    b = u - q.expandCollapseBtnRadius;
                    v += q.expandCollapseBtnRadius
                } else {
                    b = u + q.expandCollapseBtnRadius;
                    v -= q.expandCollapseBtnRadius
                }
                f = o.x + (o.w / 2);
                h = o.y;
                l = o.x + (o.w / 2);
                n = o.y + o.h;
                break;
            case getOrgChart.RO_LEFT:
            case getOrgChart.RO_LEFT_PARENT_TOP:
                t = o.x;
                v = yc = o.y + (o.h / 2);
                a = r;
                if (u > v) {
                    b = u - q.expandCollapseBtnRadius;
                    v += q.expandCollapseBtnRadius
                } else {
                    b = u + q.expandCollapseBtnRadius;
                    v -= q.expandCollapseBtnRadius
                }
                f = o.x + (o.w / 2);
                h = o.y;
                l = o.x + (o.w / 2);
                n = o.y + o.h;
                break
        }
        if (o.leftNeighbor == this) {
            p.push('<path data-link-id="' + this.id + '" class="link-second-parent"   d="M' + e + "," + g + "  L" + f + "," + h + '"/>')
        } else {
            if (o.rightNeighbor == this) {
                p.push('<path data-link-id="' + this.id + '" class="link-second-parent"   d="M' + j + "," + m + "  L" + l + "," + n + '"/>')
            } else {
                p.push('<path data-link-id="' + this.id + '" class="link-second-parent"   d="M' + a + "," + b + "  L" + t + "," + v + '"/>')
            }
        }
    }
    return p.join("")
};
getOrgChart.node.prototype.isCollapsed = function () {
    if (this.parent == null) {
        return false
    }
    if (this.parent.collapsed == getOrgChart.COLLAPSED) {
        return true
    } else {
        return this.parent.isCollapsed()
    }
    return true
};
getOrgChart.node.prototype.getMostDeepChild = function (c) {
    var b = this;

    function a(f, g) {
        if (f.collapsed == getOrgChart.EXPANDED || f.displayChildren != null) {
            for (var d = 0; d < f.getChildren().length; d++) {
                var e = g[f.getChildren()[d].id];
                if (e.level > b.level) {
                    b = e
                }
                a(f.getChildren()[d], g)
            }
        }
    }
    a(this, c);
    return b
};
getOrgChart.node.prototype.draw = function (a) {
    var h = [];
    var b = this.getImageUrl();
    var m = a.customize[this.id] && a.customize[this.id].theme ? getOrgChart.themes[a.customize[this.id].theme] : getOrgChart.themes[a.theme];
    var f = a.customize[this.id] && a.customize[this.id].theme ? " get-" + a.customize[this.id].theme : "";
    var e = a.customize[this.id] && a.customize[this.id].color ? " get-" + a.customize[this.id].color : "";
    if (f && !e) {
        e = " get-" + a.color
    }
    if (this.color != null && (this.displayChildren != null || this.displayParent != null)) {
        e += " get-" + this.color
    }
    if (e && !f) {
        f = " get-" + a.theme
    }
    var d = f + e;
    var l = b ? m.textPoints : m.textPointsNoImage;
    h.push(getOrgChart.OPEN_NODE.replace("[data-node-id]", this.id).replace("[x]", this._z2 == null ? this.x : this._z2).replace("[y]", this._z3 == null ? this.y : this._z3).replace("[level]", this.level).replace("[nodeCssClass]", d));
    for (themeProperty in m) {
        switch (themeProperty) {
            case "image":
                if (b) {
                    h.push(m.image.replace("[href]", b))
                }
                break;
            case "text":
                var j = 0;
                for (k = 0; k < a.primaryFields.length; k++) {
                    var g = l[j];
                    var c = a.primaryFields[k];
                    if (!g || !this.data || !this.data[c]) {
                        continue
                    }
                    h.push(m.text.replace("[index]", j).replace("[text]", this.data[c]).replace("[y]", g.y).replace("[x]", g.x).replace("[rotate]", g.rotate).replace("[width]", g.width));
                    j++
                }
                break;
            default:
                if (themeProperty != "size" && themeProperty != "toolbarHeight" && themeProperty != "textPoints" && themeProperty != "expandCollapseBtnRadius" && themeProperty != "textPointsNoImage") {
                    h.push(m[themeProperty])
                }
                break
        }
    }
    h.push(getOrgChart.CLOSE_NODE);
    return h
};
if (!getOrgChart) {
    var getOrgChart = {}
}
getOrgChart.themes = {
    annabel: {
        size: [500, 220],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 210,
            y: 40,
            width: 290
        }, {
            x: 210,
            y: 65,
            width: 290
        }, {
            x: 210,
            y: 90,
            width: 290
        }, {
            x: 210,
            y: 115,
            width: 290
        }, {
            x: 210,
            y: 140,
            width: 290
        }],
        textPointsNoImage: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 10,
            y: 40,
            width: 490
        }, {
            x: 10,
            y: 65,
            width: 490
        }, {
            x: 10,
            y: 90,
            width: 490
        }, {
            x: 10,
            y: 115,
            width: 490
        }, {
            x: 10,
            y: 140,
            width: 490
        }],
        expandCollapseBtnRadius: 20,
        box: '<path class="get-box" d="M0 0 L500 0 L500 220 L0 220 Z"/>',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<image xlink:href="[href]"  x="20" y="-20" height="170" preserveAspectRatio="xMidYMid slice" width="170"/>'
    },
    sara: {
        size: [500, 220],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 210,
            y: 40,
            width: 290
        }, {
            x: 210,
            y: 65,
            width: 290
        }, {
            x: 210,
            y: 90,
            width: 290
        }, {
            x: 210,
            y: 115,
            width: 290
        }, {
            x: 210,
            y: 140,
            width: 290
        }],
        textPointsNoImage: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 10,
            y: 40,
            width: 490
        }, {
            x: 10,
            y: 65,
            width: 490
        }, {
            x: 10,
            y: 90,
            width: 490
        }, {
            x: 10,
            y: 115,
            width: 490
        }, {
            x: 10,
            y: 140,
            width: 490
        }],
        expandCollapseBtnRadius: 20,
        box: '<rect  x="0" y="0" height="220" width="500" rx="10" ry="10" class="get-box" />',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<image xlink:href="[href]"  x="20" y="-20" height="170" preserveAspectRatio="xMidYMid slice" width="170"/>'
    },
    belinda: {
        size: [300, 300],
        toolbarHeight: 46,
        textPoints: [{
            x: 40,
            y: 70,
            width: 220
        }, {
            x: 40,
            y: 245,
            width: 220
        }, {
            x: 65,
            y: 270,
            width: 170
        }],
        textPointsNoImage: [{
            x: 30,
            y: 100,
            width: 240
        }, {
            x: 30,
            y: 140,
            width: 240
        }, {
            x: 30,
            y: 180,
            width: 240
        }, {
            x: 30,
            y: 220,
            width: 240
        }],
        expandCollapseBtnRadius: 20,
        box: '<circle class="get-box" cx="150" cy="150" r="150" />',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<clipPath id="getBelindaClip1"><rect x="0" y="75" width="300" height="150" /></clipPath><clipPath clip-path="url(#getBelindaClip1)" id="getBelindaClip2"><circle cx="150" cy="150" r="150" /></clipPath><image preserveAspectRatio="xMidYMid slice"  clip-path="url(#getBelindaClip2)" xlink:href="[href]" x="1" y="1" height="300"   width="300"/>'
    },
    cassandra: {
        size: [310, 140],
        toolbarHeight: 46,
        textPoints: [{
            x: 110,
            y: 50,
            width: 200
        }, {
            x: 110,
            y: 80,
            width: 200
        }, {
            x: 110,
            y: 105,
            width: 200
        }, {
            x: 110,
            y: 130,
            width: 200
        }],
        textPointsNoImage: [{
            x: 110,
            y: 50,
            width: 200
        }, {
            x: 110,
            y: 80,
            width: 200
        }, {
            x: 110,
            y: 105,
            width: 200
        }, {
            x: 110,
            y: 130,
            width: 200
        }],
        expandCollapseBtnRadius: 20,
        box: '<path class="get-box get-cassandra-border" d="M70 10 L70 0 L310 0 L310 10 M310 130 L310 140 L70 140 L70 130"/>',
        text: '<text width="[width]" class="get-text get-text-[index] get-cassandra-text" x="[x]" y="[y]">[text]</text>',
        image: '<image xlink:href="[href]" x="1" y="20" height="100" preserveAspectRatio="xMidYMid slice" width="100"/>'
    },
    deborah: {
        size: [222, 222],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 40,
            width: 202
        }, {
            x: 10,
            y: 200,
            width: 202
        }],
        textPointsNoImage: [{
            x: 10,
            y: 40,
            width: 202
        }, {
            x: 10,
            y: 200,
            width: 202
        }],
        expandCollapseBtnRadius: 20,
        image: '<clipPath id="getVivaClip"><path class="get-box" d="M35 0 L187 0 Q222 0 222 35 L222 187 Q222 222 187 222 L35 222 Q0 222 0 187 L0 35 Q0 0 35 0 Z"/></clipPath><image clip-path="url(#getVivaClip)" xlink:href="[href]" x="0" y="0" height="222" preserveAspectRatio="xMidYMid slice" width="222"/>',
        box: '<path class="get-text-pane" d="M222 172 Q222 222 187 222 L35 222 Q0 222 0 187 L0 172 Z"/><path class="get-text-pane" d="M35 0 L187 0 Q222 0 222 35 L222 50 L0 50 L0 50 Q0 0 35 0 Z"/><path class="get-box" d="M35 0 L187 0 Q222 0 222 35 L222 187 Q222 222 187 222 L35 222 Q0 222 0 187 L0 35 Q0 0 35 0 Z"/>',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>'
    },
    lena: {
        size: [481, 420],
        toolbarHeight: 46,
        textPoints: [{
            x: 40,
            y: 130,
            width: 280
        }, {
            x: 40,
            y: 325,
            width: 280
        }, {
            x: 40,
            y: 355,
            width: 280
        }, {
            x: 40,
            y: 385,
            width: 280
        }],
        textPointsNoImage: [{
            x: 40,
            y: 130,
            width: 280
        }, {
            x: 40,
            y: 190,
            width: 280
        }, {
            x: 40,
            y: 220,
            width: 280
        }, {
            x: 40,
            y: 250,
            width: 280
        }, {
            x: 40,
            y: 280,
            width: 280
        }, {
            x: 40,
            y: 310,
            width: 280
        }, {
            x: 40,
            y: 340,
            width: 280
        }],
        expandCollapseBtnRadius: 20,
        defs: '<linearGradient id="getNodeDef2"><stop class="get-def-stop-1" offset="0" /><stop class="get-def-stop-2" offset="1" /></linearGradient><linearGradient xlink:href="#getNodeDef2" id="getNodeDef1" y2="0.21591" x2="0.095527" y1="0.140963" x1="0.063497" />',
        box: '<path fill="#000000" fill-opacity="0.392157" fill-rule="nonzero" stroke-width="4" stroke-miterlimit="4" d="M15.266,67.6297 C66.2394,47.802 149.806,37.5153 149.806,37.5153 L387.9,6.06772 L413.495,199.851 C413.495,199.851 427.17,312.998 460.342,367.036 C382.729,399.222 245.307,419.23 245.307,419.23 L51.5235,444.825 L7.74078,113.339 C7.74078,113.339 0.7616,86.8934 15.266,67.6297 L15.266,67.6297 z" /><path fill="url(#getNodeDef1)" fill-rule="nonzero" stroke="#000000" stroke-width="4" stroke-miterlimit="4" d="M7.83745,60.562 C66.3108,43.7342 144.877,33.4476 144.877,33.4476 L382.972,2 L408.567,195.783 C408.567,195.783 417.334,271.777 450.506,325.814 C387.314,401.952 240.378,415.162 240.378,415.162 L46.5949,440.757 L2.81219,109.271 C2.81219,109.271 -0.98386,77.3975 7.83744,60.562 L7.83745,60.562 z" />',
        text: '<text transform="rotate(-8)" width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<image transform="rotate(-8)" xlink:href="[href]" x="40" y="150" height="150" preserveAspectRatio="xMidYMid slice" width="280"/>'
    },
    monica: {
        size: [500, 220],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 200,
            y: 40,
            width: 300
        }, {
            x: 210,
            y: 65,
            width: 290
        }, {
            x: 210,
            y: 90,
            width: 290
        }, {
            x: 200,
            y: 115,
            width: 300
        }, {
            x: 185,
            y: 140,
            width: 315
        }],
        textPointsNoImage: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 10,
            y: 40,
            width: 490
        }, {
            x: 10,
            y: 65,
            width: 490
        }, {
            x: 10,
            y: 90,
            width: 490
        }, {
            x: 10,
            y: 115,
            width: 490
        }, {
            x: 10,
            y: 140,
            width: 490
        }],
        expandCollapseBtnRadius: 20,
        box: '<path class="get-box" d="M0 0 L500 0 L500 220 L0 220 Z"/>',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<clipPath id="getMonicaClip"><circle cx="105" cy="65" r="85" /></clipPath><image preserveAspectRatio="xMidYMid slice" clip-path="url(#getMonicaClip)" xlink:href="[href]" x="20" y="-20" height="170" width="170"/>'
    },
    ula: {
        size: [500, 220],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 200,
            y: 40,
            width: 300
        }, {
            x: 210,
            y: 65,
            width: 290
        }, {
            x: 210,
            y: 90,
            width: 290
        }, {
            x: 200,
            y: 115,
            width: 300
        }, {
            x: 185,
            y: 140,
            width: 315
        }],
        textPointsNoImage: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 10,
            y: 40,
            width: 490
        }, {
            x: 10,
            y: 65,
            width: 490
        }, {
            x: 10,
            y: 90,
            width: 490
        }, {
            x: 10,
            y: 115,
            width: 490
        }, {
            x: 10,
            y: 140,
            width: 490
        }],
        expandCollapseBtnRadius: 20,
        box: '<rect x="0" y="0" height="220" width="500" rx="10" ry="10" class="get-box" />',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<clipPath id="getMonicaClip"><circle cx="105" cy="65" r="85" /></clipPath><image preserveAspectRatio="xMidYMid slice" clip-path="url(#getMonicaClip)" xlink:href="[href]" x="20" y="-20" height="170" width="170"/>'
    },
    eve: {
        size: [500, 220],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 210,
            y: 40,
            width: 290
        }, {
            x: 210,
            y: 65,
            width: 290
        }, {
            x: 210,
            y: 90,
            width: 290
        }, {
            x: 210,
            y: 115,
            width: 290
        }, {
            x: 210,
            y: 140,
            width: 290
        }],
        textPointsNoImage: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 10,
            y: 40,
            width: 490
        }, {
            x: 10,
            y: 65,
            width: 490
        }, {
            x: 10,
            y: 90,
            width: 490
        }, {
            x: 10,
            y: 115,
            width: 490
        }, {
            x: 10,
            y: 140,
            width: 490
        }],
        expandCollapseBtnRadius: 20,
        box: '<path class="get-box" d="M0 0 L500 0 L500 220 L0 220 Z"/>',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<image xlink:href="[href]" x="20" y="-20" height="170" preserveAspectRatio="xMidYMid slice" width="170"/>'
    },
    tal: {
        size: [500, 220],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 210,
            y: 40,
            width: 290
        }, {
            x: 210,
            y: 65,
            width: 290
        }, {
            x: 210,
            y: 90,
            width: 290
        }, {
            x: 210,
            y: 115,
            width: 290
        }, {
            x: 210,
            y: 140,
            width: 290
        }],
        textPointsNoImage: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 10,
            y: 40,
            width: 490
        }, {
            x: 10,
            y: 65,
            width: 490
        }, {
            x: 10,
            y: 90,
            width: 490
        }, {
            x: 10,
            y: 115,
            width: 490
        }, {
            x: 10,
            y: 140,
            width: 490
        }],
        expandCollapseBtnRadius: 20,
        box: '<rect x="0" y="0" height="220" width="500" rx="10" ry="10" class="get-box" />',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<image xlink:href="[href]" x="20" y="-20" height="170" preserveAspectRatio="xMidYMid slice" width="170"/>'
    },
    vivian: {
        size: [500, 220],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 240,
            y: 40,
            width: 260
        }, {
            x: 250,
            y: 65,
            width: 250
        }, {
            x: 270,
            y: 90,
            width: 230
        }, {
            x: 290,
            y: 115,
            width: 210
        }, {
            x: 310,
            y: 140,
            width: 290
        }],
        textPointsNoImage: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 10,
            y: 40,
            width: 490
        }, {
            x: 10,
            y: 65,
            width: 490
        }, {
            x: 10,
            y: 90,
            width: 490
        }, {
            x: 10,
            y: 115,
            width: 490
        }, {
            x: 10,
            y: 140,
            width: 490
        }],
        expandCollapseBtnRadius: 20,
        box: '<path class="get-box" d="M0 0 L500 0 L500 220 L0 220 Z"/>',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<clipPath id="getVivianClip"><polygon class="get-box" points="20,70 75,-20 185,-20 240,70 185,160 75,160"/></clipPath><image preserveAspectRatio="xMidYMid slice" clip-path="url(#getVivianClip)" xlink:href="[href]" x="20" y="-20" height="200" width="300"/>'
    },
    ada: {
        size: [500, 220],
        toolbarHeight: 46,
        textPoints: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 240,
            y: 40,
            width: 260
        }, {
            x: 250,
            y: 65,
            width: 250
        }, {
            x: 270,
            y: 90,
            width: 230
        }, {
            x: 290,
            y: 115,
            width: 210
        }, {
            x: 310,
            y: 140,
            width: 290
        }],
        textPointsNoImage: [{
            x: 10,
            y: 200,
            width: 490
        }, {
            x: 10,
            y: 40,
            width: 490
        }, {
            x: 10,
            y: 65,
            width: 490
        }, {
            x: 10,
            y: 90,
            width: 490
        }, {
            x: 10,
            y: 115,
            width: 490
        }, {
            x: 10,
            y: 140,
            width: 490
        }],
        expandCollapseBtnRadius: 20,
        box: '<rect x="0" y="0" height="220" width="500" rx="10" ry="10" class="get-box" />',
        text: '<text width="[width]" class="get-text get-text-[index]" x="[x]" y="[y]">[text]</text>',
        image: '<clipPath id="getVivianClip"><polygon class="get-box" points="20,70 75,-20 185,-20 240,70 185,160 75,160"/></clipPath><image preserveAspectRatio="xMidYMid slice" clip-path="url(#getVivianClip)" xlink:href="[href]" x="20" y="-20" height="200" width="300"/>'
    },
    helen: {
        size: [380, 190],
        toolbarHeight: 46,
        textPoints: [{
            x: 20,
            y: 170,
            width: 350,
            rotate: 0
        }, {
            x: 0,
            y: -380,
            width: 170,
            rotate: 90
        }, {
            x: 20,
            y: -5,
            width: 170,
            rotate: 0
        }],
        textPointsNoImage: [{
            x: 20,
            y: 170,
            width: 350,
            rotate: 0
        }, {
            x: 20,
            y: 115,
            width: 350,
            rotate: 0
        }, {
            x: 20,
            y: 85,
            width: 350,
            rotate: 0
        }, {
            x: 20,
            y: 55,
            width: 350,
            rotate: 0
        }, {
            x: 20,
            y: 25,
            width: 350,
            rotate: 0
        }, {
            x: 20,
            y: -5,
            width: 350,
            rotate: 0
        }],
        expandCollapseBtnRadius: 20,
        text: '<text transform="rotate([rotate])"  width="[width]" class="get-text get-text-[index] get-helen-text" x="[x]" y="[y]">[text]</text>',
        image: '<image xlink:href="[href]" x="20" y="0" height="140" preserveAspectRatio="xMidYMid slice" width="350"/>'
    }
};
if (typeof (get) == "undefined") {
    get = {}
}
get._z = function (a, c, b, h, i, d) {
    var n;
    var e = 10;
    var k = 1;
    var m = 1;
    var l = h / e + 1;
    var j = document.getElementsByTagName("g");
    if (!a.length) {
        a = [a]
    }

    function f() {
        for (var s in b) {
            var t = getOrgChart.util._s(["top", "left", "right", "bottom"], s.toLowerCase()) ? "px" : "";
            switch (s.toLowerCase()) {
                case "d":
                    var v = i(((m * e) - e) / h) * (b[s][0] - c[s][0]) + c[s][0];
                    var w = i(((m * e) - e) / h) * (b[s][1] - c[s][1]) + c[s][1];
                    for (z = 0; z < a.length; z++) {
                        a[z].setAttribute("d", a[z].getAttribute("d") + " L" + v + " " + w)
                    }
                    break;
                case "transform":
                    if (b[s]) {
                        var q = c[s];
                        var p = b[s];
                        var r = [0, 0, 0, 0, 0, 0];
                        for (var g in q) {
                            r[g] = i(((m * e) - e) / h) * (p[g] - q[g]) + q[g]
                        }
                        for (z = 0; z < a.length; z++) {
                            a[z].setAttribute("transform", "matrix(" + r.toString() + ")")
                        }
                    }
                    break;
                case "viewbox":
                    if (b[s]) {
                        var q = c[s];
                        var p = b[s];
                        var r = [0, 0, 0, 0];
                        for (g in q) {
                            r[g] = i(((m * e) - e) / h) * (p[g] - q[g]) + q[g]
                        }
                        for (z = 0; z < a.length; z++) {
                            a[z].setAttribute("viewBox", r.toString())
                        }
                    }
                    break;
                case "margin":
                    if (b[s]) {
                        var q = c[s];
                        var p = b[s];
                        var r = [0, 0, 0, 0];
                        for (g in q) {
                            r[g] = i(((m * e) - e) / h) * (p[g] - q[g]) + q[g]
                        }
                        var o = "";
                        for (g = 0; g < r.length; g++) {
                            o += parseInt(r[g]) + "px "
                        }
                        for (z = 0; z < a.length; z++) {
                            if (a[z] && a[z].style) {
                                a[z].style[s] = u
                            }
                        }
                    }
                    break;
                default:
                    var u = i(((m * e) - e) / h) * (b[s] - c[s]) + c[s];
                    for (z = 0; z < a.length; z++) {
                        if (a[z] && a[z].style) {
                            a[z].style[s] = u + t
                        }
                    }
                    break
            }
        }
        m = m + k;
        if (m > l + 1) {
            clearInterval(n);
            if (d) {
                d(a)
            }
        }
    }
    n = setInterval(f, e)
};
get._z._ag = function (b) {
    var a = 2;
    if (b < 0) {
        return 0
    }
    if (b > 1) {
        return 1
    }
    return Math.pow(b, a)
};
get._z._a6 = function (c) {
    var a = 2;
    if (c < 0) {
        return 0
    }
    if (c > 1) {
        return 1
    }
    var b = a % 2 == 0 ? -1 : 1;
    return (b * (Math.pow(c - 1, a) + b))
};
get._z._av = function (c) {
    var a = 2;
    if (c < 0) {
        return 0
    }
    if (c > 1) {
        return 1
    }
    c *= 2;
    if (c < 1) {
        return get._z._ag(c, a) / 2
    }
    var b = a % 2 == 0 ? -1 : 1;
    return (b / 2 * (Math.pow(c - 2, a) + b * 2))
};
get._z._ab = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return -Math.cos(a * (Math.PI / 2)) + 1
};
get._z._a7 = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return Math.sin(a * (Math.PI / 2))
};
get._z._at = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return -0.5 * (Math.cos(Math.PI * a) - 1)
};
get._z._ae = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return Math.pow(2, 10 * (a - 1))
};
get._z._a5 = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return -Math.pow(2, -10 * a) + 1
};
get._z._af = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return a < 0.5 ? 0.5 * Math.pow(2, 10 * (2 * a - 1)) : 0.5 * (-Math.pow(2, 10 * (-2 * a + 1)) + 2)
};
get._z._ax = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return -(Math.sqrt(1 - a * a) - 1)
};
get._z._a4 = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return Math.sqrt(1 - (a - 1) * (a - 1))
};
get._z._ar = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return a < 1 ? -0.5 * (Math.sqrt(1 - a * a) - 1) : 0.5 * (Math.sqrt(1 - ((2 * a) - 2) * ((2 * a) - 2)) + 1)
};
get._z._zb = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    if (a < (1 / 2.75)) {
        return 1 - 7.5625 * a * a
    } else {
        if (a < (2 / 2.75)) {
            return 1 - (7.5625 * (a - 1.5 / 2.75) * (a - 1.5 / 2.75) + 0.75)
        } else {
            if (a < (2.5 / 2.75)) {
                return 1 - (7.5625 * (a - 2.25 / 2.75) * (a - 2.25 / 2.75) + 0.9375)
            } else {
                return 1 - (7.5625 * (a - 2.625 / 2.75) * (a - 2.625 / 2.75) + 0.984375)
            }
        }
    }
};
get._z._as = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return a * a * ((1.70158 + 1) * a - 1.70158)
};
get._z._a3 = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return (a - 1) * (a - 1) * ((1.70158 + 1) * (a - 1) + 1.70158) + 1
};
get._z._ac = function (a) {
    if (a < 0) {
        return 0
    }
    if (a > 1) {
        return 1
    }
    return a < 0.5 ? 0.5 * (4 * a * a * ((2.5949 + 1) * 2 * a - 2.5949)) : 0.5 * ((2 * a - 2) * (2 * a - 2) * ((2.5949 + 1) * (2 * a - 2) + 2.5949) + 2)
};
get._z._aw = function (c) {
    var b = 2;
    var a = b * c;
    return a * Math.exp(1 - a)
};
get._z._R = function (c) {
    var a = 2;
    var b = 2;
    return Math.exp(-a * Math.pow(c, b))
};
if (!getOrgChart) {
    var getOrgChart = {}
}
getOrgChart.buttons = {
    add: '<g style="display:none;" class="get-btn" data-btn-id="[id]" data-btn-action="add" transform="matrix(0.14,0,0,0.14,0,0)"><rect style="opacity:0" x="0" y="0" height="300" width="300" /><path  fill="#686868" d="M149.996,0C67.157,0,0.001,67.158,0.001,149.997c0,82.837,67.156,150,149.995,150s150-67.163,150-150 C299.996,67.156,232.835,0,149.996,0z M149.996,59.147c25.031,0,45.326,20.292,45.326,45.325 c0,25.036-20.292,45.328-45.326,45.328s-45.325-20.292-45.325-45.328C104.671,79.439,124.965,59.147,149.996,59.147z M168.692,212.557h-0.001v16.41v2.028h-18.264h-0.864H83.86c0-44.674,24.302-60.571,40.245-74.843 c7.724,4.15,16.532,6.531,25.892,6.601c9.358-0.07,18.168-2.451,25.887-6.601c7.143,6.393,15.953,13.121,23.511,22.606h-7.275 v10.374v13.051h-13.054h-10.374V212.557z M218.902,228.967v23.425h-16.41v-23.425h-23.428v-16.41h23.428v-23.425H218.9v23.425 h23.423v16.41H218.902z"/></g>',
    edit: '<g style="display:none;" class="get-btn" data-btn-id="[id]" data-btn-action="edit" transform="matrix(0.14,0,0,0.14,0,0)"><rect style="opacity:0" x="0" y="0" height="300" width="300" /><path fill="#686868" d="M149.996,0C67.157,0,0.001,67.161,0.001,149.997S67.157,300,149.996,300s150.003-67.163,150.003-150.003 S232.835,0,149.996,0z M221.302,107.945l-14.247,14.247l-29.001-28.999l-11.002,11.002l29.001,29.001l-71.132,71.126 l-28.999-28.996L84.92,186.328l28.999,28.999l-7.088,7.088l-0.135-0.135c-0.786,1.294-2.064,2.238-3.582,2.575l-27.043,6.03 c-0.405,0.091-0.817,0.135-1.224,0.135c-1.476,0-2.91-0.581-3.973-1.647c-1.364-1.359-1.932-3.322-1.512-5.203l6.027-27.035 c0.34-1.517,1.286-2.798,2.578-3.582l-0.137-0.137L192.3,78.941c1.678-1.675,4.404-1.675,6.082,0.005l22.922,22.917 C222.982,103.541,222.982,106.267,221.302,107.945z"/></g>',
    del: '<g style="display:none;" class="get-btn" data-btn-id="[id]" data-btn-action="del" transform="matrix(0.14,0,0,0.14,0,0)"><rect style="opacity:0" x="0" y="0" height="300" width="300" /><path fill="#686868" d="M112.782,205.804c10.644,7.166,23.449,11.355,37.218,11.355c36.837,0,66.808-29.971,66.808-66.808 c0-13.769-4.189-26.574-11.355-37.218L112.782,205.804z"/> <path stroke="#686868" fill="#686868" d="M150,83.542c-36.839,0-66.808,29.969-66.808,66.808c0,15.595,5.384,29.946,14.374,41.326l93.758-93.758 C179.946,88.926,165.595,83.542,150,83.542z"/><path stroke="#686868" fill="#686868" d="M149.997,0C67.158,0,0.003,67.161,0.003,149.997S67.158,300,149.997,300s150-67.163,150-150.003S232.837,0,149.997,0z M150,237.907c-48.28,0-87.557-39.28-87.557-87.557c0-48.28,39.277-87.557,87.557-87.557c48.277,0,87.557,39.277,87.557,87.557 C237.557,198.627,198.277,237.907,150,237.907z"/></g>',
    details: '<g style="display:none;" class="get-btn" data-btn-id="[id]" data-btn-action="details" transform="matrix(0.14,0,0,0.14,0,0)"><rect style="opacity:0" x="0" y="0" height="300" width="300" /><path fill="#686868" d="M139.414,96.193c-22.673,0-41.056,18.389-41.056,41.062c0,22.678,18.383,41.062,41.056,41.062 c22.678,0,41.059-18.383,41.059-41.062C180.474,114.582,162.094,96.193,139.414,96.193z M159.255,146.971h-12.06v12.06 c0,4.298-3.483,7.781-7.781,7.781c-4.298,0-7.781-3.483-7.781-7.781v-12.06h-12.06c-4.298,0-7.781-3.483-7.781-7.781 c0-4.298,3.483-7.781,7.781-7.781h12.06v-12.063c0-4.298,3.483-7.781,7.781-7.781c4.298,0,7.781,3.483,7.781,7.781v12.063h12.06 c4.298,0,7.781,3.483,7.781,7.781C167.036,143.488,163.555,146.971,159.255,146.971z"/><path stroke="#686868" fill="#686868" d="M149.997,0C67.157,0,0.001,67.158,0.001,149.995s67.156,150.003,149.995,150.003s150-67.163,150-150.003 S232.836,0,149.997,0z M225.438,221.254c-2.371,2.376-5.48,3.561-8.59,3.561s-6.217-1.185-8.593-3.561l-34.145-34.147 c-9.837,6.863-21.794,10.896-34.697,10.896c-33.548,0-60.742-27.196-60.742-60.744c0-33.548,27.194-60.742,60.742-60.742 c33.548,0,60.744,27.194,60.744,60.739c0,11.855-3.408,22.909-9.28,32.256l34.56,34.562 C230.185,208.817,230.185,216.512,225.438,221.254z"/></g>',
    expColl: '<g transform="matrix(1,0,0,1,[xa],[ya])" data-btn-id="[id]" data-btn-action="expColl"><circle cx="[middle]" cy="[middle]" r="[middle]" class="get-btn" /><line x1="[start]" y1="[middle]" x2="[end]" y2="[middle]" class="get-btn get-btn-line" /><line style="display:[display]" x1="[middle]" y1="[start]" x2="[middle]" y2="[end]" class="get-btn get-btn-line" /></g>'
};
getOrgChart.buttons.draw = function () {
    var a = [];
    a.push(getOrgChart.buttons.details);
    a.push(getOrgChart.buttons.edit);
    a.push(getOrgChart.buttons.add);
    a.push(getOrgChart.buttons.del);
    return a
};
if (typeof (get) == "undefined") {
    get = {}
}
get._c = function () {
    if (getOrgChart._c) {
        return getOrgChart._c
    }
    var g = navigator.userAgent;
    g = g.toLowerCase();
    var f = /(webkit)[ \/]([\w.]+)/;
    var e = /(opera)(?:.*version)?[ \/]([\w.]+)/;
    var d = /(msie) ([\w.]+)/;
    var c = /(mozilla)(?:.*? rv:([\w.]+))?/;
    var b = f.exec(g) || e.exec(g) || d.exec(g) || g.indexOf("compatible") < 0 && c.exec(g) || [];
    var a = {
        browser: b[1] || "",
        version: b[2] || "0"
    };
    getOrgChart._c = {
        msie: (navigator.userAgent.indexOf("Trident") != -1),
        webkit: a.browser == "webkit",
        mozilla: a.browser == "mozilla",
        opera: a.browser == "opera"
    };
    return getOrgChart._c
};
getOrgChart.util = {};
getOrgChart.util._8 = function (a) {
    var b = a._v.getAttribute("viewBox");
    b = "[" + b + "]";
    b = b.replace(/\ /g, ",");
    b = JSON.parse(b);
    return b
};
getOrgChart.util._6 = function (a) {
    var b = a.getAttribute("transform");
    b = b.replace("matrix", "").replace("(", "").replace(")", "");
    if (get._c().msie) {
        b = b.replace(/ /g, ",")
    }
    b = getOrgChart.util._zM(b);
    b = "[" + b + "]";
    b = JSON.parse(b);
    return b
};
getOrgChart.util._zM = function (a) {
    return a.replace(/^\s+|\s+$/g, "")
};
getOrgChart.util._s = function (a, c) {
    if (a && Array.isArray(a)) {
        var b = a.length;
        while (b--) {
            if (a[b] == c) {
                return true
            }
        }
    }
    return false
};
getOrgChart.util._G = function (b) {
    var a = "1";
    while (b[a]) {
        a++
    }
    return a
};
getOrgChart.util._7 = function (f) {
    var h = [],
        c;
    var d = window.location.href.slice(window.location.href.indexOf("?") + 1).split("&");
    for (var e = 0; e < d.length; e++) {
        c = d[e].split("=");
        if (c && c.length == 2 && c[0] === f) {
            var a, b;
            var g = /(%[^%]{2})/;
            while ((encodedChar = g.exec(c[1])) != null && encodedChar.length > 1 && encodedChar[1] != "") {
                a = parseInt(encodedChar[1].substr(1), 16);
                b = String.fromCharCode(a);
                c[1] = c[1].replace(encodedChar[1], b)
            }
            return decodeURIComponent(escape(c[1]))
        }
    }
    return null
};
getOrgChart.util._zG = function (c) {
    if (window.ActiveXObject) {
        var a = new ActiveXObject("Microsoft.XMLDOM");
        a.async = "false";
        a.loadXML(c)
    } else {
        var b = new DOMParser();
        var a = b.parseFromString(c, "text/xml")
    }
    return a
};
getOrgChart.util._au = function (a) {
    if (a == null || typeof (a) == "undefined" || a == "" || a == -1) {
        return true
    }
    return false
};
getOrgChart.util._ah = function (a) {
    return (typeof a !== "undefined" && a !== null)
};
getOrgChart.util._zq = function (b, a) {
    var c = b.getBoundingClientRect();
    var d = a.touches[0].pageX - c.left;
    var f = a.touches[0].pageY - c.top;
    var g = a.touches[1].pageX - c.left;
    var h = a.touches[1].pageY - c.top;
    return Math.sqrt((d - g) * (d - g) + (f - h) * (f - h))
};
getOrgChart.util._zz = function (b, a) {
    var c = b.getBoundingClientRect();
    var g = a.touches[0].pageX - c.left;
    var h = a.touches[0].pageY - c.top;
    var i = a.touches[1].pageX - c.left;
    var j = a.touches[1].pageY - c.top;
    var d = ((g - i) / 2 + i) / (c.width / 100);
    var f = ((h - j) / 2 + j) / (c.height / 100);
    return [d, f]
};
getOrgChart.util._aC = function (a, b, c) {
    var d = a.getBoundingClientRect();
    var g = b - d.left;
    var h = c - d.top;
    var e = (g) / (d.width / 100);
    var f = (h) / (d.height / 100);
    return [e, f]
};
getOrgChart.util._zJ = function (b, a) {
    var c = b.getBoundingClientRect();
    var d = a.touches[0].pageX - c.left;
    var f = a.touches[0].pageY - c.top;
    return Math.sqrt((d - t2x) * (d - t2x) + (f - t2y) * (f - t2y))
};
getOrgChart.util._4 = function (a) {
    var b = ["darkred", "pink", "darkorange", "orange", "lightgreen", "green", "lightteal", "teal", "lightblue", "blue", "darkpurple", "purple", "mediumdarkblue", "darkblue", "cordovan", "darkcordovan", "neutralgrey"];
    var c = b.indexOf(a);
    b.splice(c, 1);
    var d = Math.floor((Math.random() * 16) + 1);
    return b[d]
};
getOrgChart.util._ap = function (b, a) {
    var c = b.parent;
    if (a >= 0) {
        a++
    } else {
        a = 0
    }
    if (c) {
        return getOrgChart.util._ap(c, a)
    } else {
        return a
    }
};
get._c = function () {
    if (getOrgChart._c) {
        return getOrgChart._c
    }
    var g = navigator.userAgent;
    g = g.toLowerCase();
    var f = /(webkit)[ \/]([\w.]+)/;
    var e = /(opera)(?:.*version)?[ \/]([\w.]+)/;
    var d = /(msie) ([\w.]+)/;
    var c = /(mozilla)(?:.*? rv:([\w.]+))?/;
    var b = f.exec(g) || e.exec(g) || d.exec(g) || g.indexOf("compatible") < 0 && c.exec(g) || [];
    var a = {
        browser: b[1] || "",
        version: b[2] || "0"
    };
    getOrgChart._c = {
        msie: (navigator.userAgent.indexOf("Trident") != -1),
        webkit: a.browser == "webkit",
        mozilla: a.browser == "mozilla",
        opera: a.browser == "opera"
    };
    return getOrgChart._c
};
getOrgChart.prototype.showDetailsView = function (e) {
    var j = this.nodes[e];
    var g = (j.parent == this._zu);
    var b = function (r, p, s) {
        var n = g ? 'style="display:none;"' : "";
        var t = "<select " + n + 'class="get-oc-select-parent"><option value="' + r + '">--select parent--</option>';
        var q = null;
        for (var m in p) {
            q = p[m];
            if (j == q) {
                continue
            }
            var u = "";
            for (i = 0; i < s.length; i++) {
                var o = s[i];
                if (!q.data || !q.data[o]) {
                    continue
                }
                if (u) {
                    u = u + ", " + q.data[o]
                } else {
                    u += q.data[o]
                }
            }
            if (q.id == r) {
                t += '<option selected="selected" value="' + q.id + '">' + u + "</option>"
            } else {
                t += '<option value="' + q.id + '">' + u + "</option>"
            }
        }
        t += "</select>";
        return t
    };
    var a = function (n, m) {
        var p = '<select class="get-oc-labels"><option value="">--other--</option>';
        var o;
        for (i = 0; i < m.length; i++) {
            if (!getOrgChart.util._s(n, m[i])) {
                o += '<option value="' + m[i] + '">' + m[i] + "</option>"
            }
        }
        p += o;
        p += "</select>";
        if (!o) {
            p = ""
        }
        return p
    };
    var d = "";
    var h = [];
    d += b(j.pid, this.nodes, this.config.primaryFields);
    d += getOrgChart.DETAILS_VIEW_ID_INPUT.replace("[personId]", j.id);
    for (label in j.data) {
        if (this.config.useFileUploader && getOrgChart.util._s(this.config.photoFields, label)) {
            d += getOrgChart.DETAILS_VIEW_INPUT_HTML.replace(/\[label]/g, label).replace("[file]", '<input type="file">').replace("[value]", j.data[label])
        } else {
            d += getOrgChart.DETAILS_VIEW_INPUT_HTML.replace(/\[label]/g, label).replace("[file]", "").replace("[value]", j.data[label])
        }
        h.push(label)
    }
    d += a(h, this._ai);
    this._X._i.innerHTML = d;
    var c = this._X._U();
    var k = this;
    if (c) {
        c.addEventListener("change", function () {
            var m = this.previousSibling;
            k._V("changeFileInputEvent", {
                input: this,
                callback: function (n) {
                    m.value = n;
                    k._X._m.innerHTML = getOrgChart.DETAILS_VIEW_ID_IMAGE.replace("[src]", n)
                }
            })
        })
    }
    var f = j.getImageUrl ? j.getImageUrl() : "";
    if (f) {
        this._X._m.innerHTML = getOrgChart.DETAILS_VIEW_ID_IMAGE.replace("[src]", f)
    } else {
        this._X._m.innerHTML = getOrgChart.DETAILS_VIEW_USER_LOGO
    }
    this._k();
    var l = this.config.customize[j.id] && this.config.customize[j.id].theme ? getOrgChart.themes[this.config.customize[j.id].theme].toolbarHeight : this.theme.toolbarHeight;
    this._X._t.style.top = "-9999px";
    this._X._t.style.left = "-9999px";
    this._X._u.style.top = l + "px";
    this._X._u.style.left = "0px";
    this._X._u.style.position = "relative";
    this._X._0.style.top = "-9999px";
    this._X._0.style.left = "-9999px";
    this._X._0.innerHTML = "";
    this._X._i.style.opacity = 0;
    this._X._m.style.opacity = 0;
    get._z(this._X._m, {
        opacity: 0
    }, {
            opacity: 1
        }, 400, get._z._a7);
    get._z(this._X._zU, {
        top: 0
    }, {
            top: -l
        }, 200, get._z._a7);
    get._z(this._X._i, {
        opacity: 0
    }, {
            opacity: 1
        }, 200, get._z._ab)
};
getOrgChart.prototype._k = function () {
    var a = this._X._K();
    if (this._X._M()) {
        this._q(this._X._M(), "change", this._j)
    }
};
getOrgChart.prototype._j = function (m, a) {
    var n = this._X._J();
    var l = this._X._M();
    var k = l.value;
    for (var d = 0; d < l.options.length; d++) {
        if (k == l.options[d].value) {
            l.options[d] = null
        }
    }
    if (!k) {
        return
    }
    var c = this._X._i.innerHTML;
    var f = "";
    if (this.config.useFileUploader && getOrgChart.util._s(this.config.photoFields, k)) {
        f = getOrgChart.DETAILS_VIEW_INPUT_HTML.replace(/\[label]/g, k).replace("[file]", '<input type="file">').replace("[value]", "")
    } else {
        f = getOrgChart.DETAILS_VIEW_INPUT_HTML.replace(/\[label]/g, k).replace("[file]", "").replace("[value]", "")
    }
    var e = c.indexOf('<select class="get-oc-labels">');
    this._X._i.innerHTML = c.substring(0, e) + f + c.substring(e, c.length);
    var b = this._X._U();
    var o = this;
    if (b) {
        b.addEventListener("change", function () {
            var j = this.previousSibling;
            o._V("changeFileInputEvent", {
                input: this,
                callback: function (p) {
                    j.value = p;
                    o._X._m.innerHTML = getOrgChart.DETAILS_VIEW_ID_IMAGE.replace("[src]", p)
                }
            })
        })
    }
    var g = this._X._K();
    var h = 0;
    for (d in n) {
        g[h].value = n[d];
        h++
    }
    this._k()
};
getOrgChart.prototype._zi = function (e, a) {
    if (this._S) {
        var b = this._X._I().value;
        var d;
        if (this._X._O() && this._X._O().value) {
            d = this._X._O().value
        }
        var c = this._X._J();
        this.updateNode(b, d, c);
        this._S = false
    }
    this.showMainView()
};
getOrgChart.prototype._zV = function () {
    this.showGridView()
};
getOrgChart.prototype.showGridView = function () {
    var a = '<table cellpadding="0" cellspacing="0" border="0">';
    a += "<tr>";
    a += "<th>ID</th><th>Parent ID</th>";
    for (i = 0; i < this._ai.length; i++) {
        var c = this._ai[i];
        a += "<th>" + c + "</th>"
    }
    a += "</tr>";
    for (var b in this.nodes) {
        var d = this.nodes[b];
        var f = (i % 2 == 0) ? "get-even" : "get-odd";
        var e = d.data;
        a += '<tr class="' + f + '">';
        a += "<td>" + d.id + "</td>";
        a += "<td>" + d.pid + "</td>";
        for (j = 0; j < this._ai.length; j++) {
            var c = this._ai[j];
            var g = e[c];
            a += "<td>";
            a += g ? g : "&nbsp;";
            a += "</td>"
        }
        a += "</tr>"
    }
    a += "</table>";
    this._X._0.innerHTML = a;
    this._X._t.style.top = "-9999px";
    this._X._t.style.left = "-9999px";
    this._X._u.style.top = "-9999px";
    this._X._u.style.left = "-9999px";
    this._X._0.style.top = this.theme.toolbarHeight + "px";
    this._X._0.style.left = "0px";
    get._z(this._X._0, {
        left: 100,
        opacity: 0
    }, {
            left: 0,
            opacity: 1
        }, 200, get._z._a5);
    get._z(this._X._zU, {
        top: 0
    }, {
            top: -this.theme.toolbarHeight * 2
        }, 200, get._z._a7)
};
getOrgChart.prototype._zT = function (b, a) {
    this.showMainView()
};
getOrgChart.prototype.showMainView = function () {
    this._X._t.style.top = this.theme.toolbarHeight + "px";
    this._X._t.style.left = "0px";
    this._X._u.style.top = "-9999px";
    this._X._u.style.left = "-9999px";
    this._X._0.style.top = "-9999px";
    this._X._0.style.left = "-9999px";
    this._X._0.innerHTML = "";
    if (this._X._zU.style.top != 0 && this._X._zU.style.top != "" && this._X._zU.style.top != "0px") {
        get._z(this._X._zU, {
            top: -46
        }, {
                top: 0
            }, 200, get._z._a7)
    }
};
getOrgChart.prototype._zg = function (b, a) {
    this.print()
};
getOrgChart.prototype.print = function () {
    var b = this,
        d = this._X.element,
        k = this._X._zN,
        g = [],
        h = d.parentNode,
        j = k.style.display,
        a = document.body,
        c = a.childNodes,
        e;
    if (b._an) {
        return
    }
    b._an = true;
    for (e = 0; e < c.length; e++) {
        var f = c[e];
        if (f.nodeType === 1) {
            g[e] = f.style.display;
            f.style.display = "none"
        }
    }
    k.style.display = "none";
    a.appendChild(d);
    window.focus();
    window.print();
    setTimeout(function () {
        h.appendChild(d);
        for (e = 0; e < c.length; e++) {
            var i = c[e];
            if (i.nodeType === 1) {
                i.style.display = g[e]
            }
        }
        k.style.display = j;
        b._an = false
    }, 1000)
};
getOrgChart.prototype._z6 = function () {
    this.zoom(true, true)
};
getOrgChart.prototype._z8 = function () {
    this.zoom(false, true)
};
getOrgChart.prototype._zl = function (b, a) {
    this._zp = this._X._u.scrollTop + a[0].touches[0].pageY
};
getOrgChart.prototype._zo = function (b, a) {
    this._X._u.scrollTop = this._zp - a[0].touches[0].pageY
};
getOrgChart.prototype._zQ = function (d, b) {
    this._X._n = undefined;
    var a = b[0].wheelDelta ? b[0].wheelDelta / 40 : b[0].detail ? -b[0].detail : 0;
    if (a) {
        var e = a > 0;
        var c = getOrgChart.util._aC(this._X._v, b[0].pageX, b[0].pageY);
        this.zoom(e, false, c)
    }
    return b[0].preventDefault() && false
};
getOrgChart.prototype._zh = function (b, a) {
    var c = this;
    window.setTimeout(function () {
        c.resize()
    }, 300)
};
getOrgChart.prototype._aE = function (g, b) {
    this._X._n = undefined;
    if (this._a8.dragStart) {
        this._X._t.style.cursor = "move";
        var e = (b[0].pageX - this._X._t.offsetLeft);
        var f = (b[0].pageY - this._X._t.offsetTop);
        var j = getOrgChart.util._8(this._X);
        var k = j[2] / this._a0;
        var c = j[3] / this._a9;
        var a = k > c ? k : c;
        j[0] = -((e - this._a8.dragStart.x) * a) + this._a8.dragStart.viewBoxLeft;
        j[1] = -((f - this._a8.dragStart.y) * a) + this._a8.dragStart.viewBoxTop;
        j[0] = parseInt(j[0]);
        j[1] = parseInt(j[1]);
        this._X._v.setAttribute("viewBox", j.toString())
    }
    if (this.config.enableMove) {
        var i = this;
        if (this._aV) {
            clearTimeout(this._aV);
            this._aV = setTimeout(d, 4000);
            return
        }

        function h() {
            get._z(i._X._zn, {
                right: -30,
                opacity: 0
            }, {
                    right: 1,
                    opacity: 0.8
                }, 500, get._z._ae);
            get._z(i._X._al, {
                left: -30,
                opacity: 0
            }, {
                    left: 1,
                    opacity: 0.8
                }, 500, get._z._ae);
            get._z(i._X._zK, {
                top: 19,
                opacity: 0
            }, {
                    top: 49,
                    opacity: 0.8
                }, 500, get._z._ae);
            get._z(i._X._Q, {
                bottom: -30,
                opacity: 0
            }, {
                    bottom: 1,
                    opacity: 0.8
                }, 500, get._z._ae)
        }

        function d() {
            get._z(i._X._zn, {
                right: 1,
                opacity: 0.8
            }, {
                    right: -30,
                    opacity: 0
                }, 500, get._z._ae, function () {
                    i._aV = null
                });
            get._z(i._X._al, {
                left: 1,
                opacity: 0.8
            }, {
                    left: -30,
                    opacity: 0
                }, 500, get._z._ae, function () {
                    i._aV = null
                });
            get._z(i._X._zK, {
                top: 49,
                opacity: 0.8
            }, {
                    top: 19,
                    opacity: 0
                }, 500, get._z._ae, function () {
                    i._aV = null
                });
            get._z(i._X._Q, {
                bottom: 1,
                opacity: 0.8
            }, {
                    bottom: -30,
                    opacity: 0
                }, 500, get._z._ae, function () {
                    i._aV = null
                })
        }
        h();
        this._aV = setTimeout(d, 4000)
    }
};
getOrgChart.prototype._aX = function (b, a) {
    document.body.style.mozUserSelect = document.body.style.webkitUserSelect = document.body.style.userSelect = "none";
    var c = getOrgChart.util._8(this._X);
    this._a8.dragStart = {
        x: (a[0].pageX - this._X._t.offsetLeft),
        y: (a[0].pageY - this._X._t.offsetTop),
        viewBoxLeft: c[0],
        viewBoxTop: c[1]
    }
};
getOrgChart.prototype._aD = function (b, a) {
    this._a8.dragStart = null;
    this._X._t.style.cursor = "default"
};
getOrgChart.prototype.zoom = function (c, a, b) {
    if (this._z4) {
        return false
    }
    this._z4 = true;
    var i = this;
    var j = getOrgChart.util._8(this._X);
    var f = j.slice(0);
    var h = j[2];
    var g = j[3];
    if (c === true) {
        j[2] = j[2] / (getOrgChart.SCALE_FACTOR * 1.2);
        j[3] = j[3] / (getOrgChart.SCALE_FACTOR * 1.2)
    } else {
        if (c === false) {
            j[2] = j[2] * (getOrgChart.SCALE_FACTOR * 1.2);
            j[3] = j[3] * (getOrgChart.SCALE_FACTOR * 1.2)
        } else {
            j[2] = j[2] / (c);
            j[3] = j[3] / (c)
        }
    }
    if (!b) {
        b = [50, 50]
    }
    j[0] = f[0] - (j[2] - h) / (100 / b[0]);
    j[1] = f[1] - (j[3] - g) / (100 / b[1]);
    var d = this._a0 / j[2];
    var e = this._a9 / j[3];
    this.scale = d > e ? e : d;
    if (this.scale < this.minScale) {
        this.scale = this.minScale;
        j[2] = this._a0 / this.scale;
        j[3] = this._a9 / this.scale;
        j[0] = f[0] - (j[2] - h) / (100 / b[0]);
        j[1] = f[1] - (j[3] - g) / (100 / b[1])
    }
    if (this.scale > this.maxScale) {
        this.scale = this.maxScale;
        j[2] = this._a0 / this.scale;
        j[3] = this._a9 / this.scale;
        j[0] = f[0] - (j[2] - h) / (100 / b[0]);
        j[1] = f[1] - (j[3] - g) / (100 / b[1])
    }
    if (a) {
        get._z(this._X._v, {
            viewBox: f
        }, {
                viewBox: j
            }, 300, get._z._a3, function () {
                i._z4 = false
            })
    } else {
        this._X._v.setAttribute("viewBox", j.toString());
        this._z4 = false
    }
    return false
};
getOrgChart.prototype._aG = function (c, b) {
    if (c.className.indexOf("get-disabled") > -1) {
        return false
    }
    var a = this;
    clearTimeout(this._zA.timer);
    this._zA.timer = setTimeout(function () {
        a._zA.currentIndex++;
        a._l();
        a._az()
    }, 100)
};
getOrgChart.prototype._zf = function (c, b) {
    if (c.className.indexOf("get-disabled") > -1) {
        return false
    }
    var a = this;
    clearTimeout(this._zA.timer);
    this._zA.timer = setTimeout(function () {
        a._zA.currentIndex--;
        a._l();
        a._az()
    }, 100)
};
getOrgChart.prototype._zW = function (c, b) {
    var a = this;
    clearTimeout(this._zA.timer);
    this._zA.timer = setTimeout(function () {
        a._zA.found = a._F(a._X._zZ.value);
        a._zA.currentIndex = 0;
        a._l();
        a._am();
        a._az()
    }, 500)
};
getOrgChart.prototype._zS = function (c, b) {
    var a = this;
    clearTimeout(this._zA.timer);
    this._zA.timer = setTimeout(function () {
        a._zA.currentIndex = 0;
        a._zA.found = a._F(a._X._zZ.value);
        a._am();
        a._l();
        a._az()
    }, 100)
};
getOrgChart.prototype._az = function () {
    if (this._zA.found.length) {
        this.highlightNode(this._zA.found[this._zA.currentIndex].node.id)
    }
};
getOrgChart.prototype.highlightNode = function (c) {
    var a = this;

    function b() {
        var d = a.nodes[c];
        var e = getOrgChart.util._8(a._X);
        var f = d.x - a.initialViewBoxMatrix[2] / 2 + d.w / 2;
        var g = d.y - a.initialViewBoxMatrix[3] / 2 + d.h / 2;
        a.move([f, g, a.initialViewBoxMatrix[2], a.initialViewBoxMatrix[3]], null, function () {
            var i = a._X.getNodeById(c);
            var h = getOrgChart.util._6(i);
            var j = h.slice(0);
            j[0] = getOrgChart.HIGHLIGHT_SCALE_FACTOR;
            j[3] = getOrgChart.HIGHLIGHT_SCALE_FACTOR;
            j[4] = j[4] - ((d.w / 2) * (getOrgChart.HIGHLIGHT_SCALE_FACTOR - 1));
            j[5] = j[5] - ((d.h / 2) * (getOrgChart.HIGHLIGHT_SCALE_FACTOR - 1));
            get._z(i, {
                transform: h
            }, {
                    transform: j
                }, 200, get._z._ab, function (k) {
                    get._z(k[0], {
                        transform: j
                    }, {
                            transform: h
                        }, 200, get._z._a7, function () { })
                })
        })
    }
    if (this.isCollapsed(this.nodes[c])) {
        this.expand(this.nodes[c].parent, b)
    } else {
        b()
    }
};
getOrgChart.prototype._am = function (a) { };
getOrgChart.prototype._l = function () {
    if ((this._zA.currentIndex < this._zA.found.length - 1) && (this._zA.found.length != 0)) {
        this._X._aT.className = this._X._aT.className.replace(" get-disabled", "")
    } else {
        if (this._X._aT.className.indexOf(" get-disabled") == -1) {
            this._X._aT.className = this._X._aT.className + " get-disabled"
        }
    }
    if ((this._zA.currentIndex != 0) && (this._zA.found.length != 0)) {
        this._X._zr.className = this._X._zr.className.replace(" get-disabled", "")
    } else {
        if (this._X._zr.className.indexOf(" get-disabled") == -1) {
            this._X._zr.className = this._X._zr.className + " get-disabled"
        }
    }
};
getOrgChart.prototype._F = function (g) {
    var e = [];
    if (g == "") {
        return e
    }
    if (g.toLowerCase) {
        g = g.toLowerCase()
    }
    for (var b in this.nodes) {
        var f = this.nodes[b];
        for (m in f.data) {
            if (m == this.config.photoFields[0]) {
                continue
            }
            var c = -1;
            if (getOrgChart.util._ah(f) && getOrgChart.util._ah(f.data[m])) {
                var d = f.data[m].toString().toLowerCase();
                c = d.indexOf(g)
            }
            if (c > -1) {
                e.push({
                    indexOf: c,
                    node: f
                });
                break
            }
        }
    }

    function a(h, i) {
        if (h.indexOf < i.indexOf) {
            return -1
        }
        if (h.indexOf > i.indexOf) {
            return 1
        }
        return 0
    }
    e.sort(a);
    return e
};
getOrgChart.prototype._aK = function (g, a) {
    var c = g.getAttribute("data-node-id");
    var e = this.nodes[c];
    var f = e.x + e.w - 15;
    var d = e.x - 30;
    var h = e.y - 30;
    var b = e.y + e.h - 15;
    if (this.config.enableDetailsView) {
        this._zD("details", f, h, c)
    }
    if (this.config.enableEdit == true || getOrgChart.util._s(this.config.enableEdit, c)) {
        this._zD("add", f, b, c);
        this._zD("edit", d, h, c);
        this._zD("del", d, b, c)
    }
};
getOrgChart.prototype._aI = function (i, c) {
    var b = c[0];
    b.preventDefault();
    this._aK(i, c);
    var h = new Date().getTime();
    var k = this;
    if (this._aM == null) {
        this._aM = {
            time: h
        };
        return
    }
    if (b.touches && b.touches.length && b.touches.length > 1) {
        this._aM = null;
        return
    }
    if (!this.config.enableZoomOnNodeDoubleClick) {
        this._aM = null;
        return
    }
    var l = h - this._aM.time;
    if ((l < 400) && (l > 0)) {
        this._a8.dragStart = null;
        var d = i.getAttribute("data-node-id");
        var g = this.nodes[d];
        var m = getOrgChart.util._8(this._X);
        var f = m.slice(0);
        if (!this._aM.viewBox || this._aM.id != d) {
            var f = getOrgChart.util._8(this._X);
            var a = f[2] / f[3];
            f[2] = g.w * 1.5;
            f[3] = f[2] / a;
            f[0] = (g.x - (g.w / 4));
            f[1] = (g.y - (f[3] / 2) + g.h / 2);
            this.move(f);
            if (this._aM.viewBox) {
                m = this._aM.viewBox
            }
            this._aM = {
                id: d,
                viewBox: m
            }
        } else {
            this.move(this._aM.viewBox);
            this._aM = null
        }
    }
    if (this._aM) {
        this._aM.time = h
    }
};
getOrgChart.prototype._zD = function (a, d, e, c) {
    var b = this._X.getButtonByType(a);
    b.style.display = "block";
    b.setAttribute("transform", "matrix(0.14,0,0,0.14," + d + "," + e + ")");
    b.setAttribute("data-btn-id", c)
};
getOrgChart.prototype._aH = function (d, a) {
    var b = d.getAttribute("data-node-id");
    var c = this.nodes[b];
    if (!this._V("clickNodeEvent", {
        node: c
    })) {
        return
    }
};
getOrgChart.prototype._aY = function (d, b) {
    b[0].preventDefault();
    var c = d.getAttribute("data-btn-id");
    var a = d.getAttribute("data-btn-action");
    if (a == "del") {
        this.removeNode(c)
    } else {
        if (a == "add") {
            this.insertNode(c)
        } else {
            if (a == "details") {
                this.showDetailsView(c)
            } else {
                if (a == "edit") {
                    this.showEditView(c)
                } else {
                    if (a == "expColl") {
                        this.expandOrCollapse(c)
                    }
                }
            }
        }
    }
};
getOrgChart.prototype.showEditView = function (a) {
    this._S = true;
    this.showDetailsView(a)
};
getOrgChart.prototype.expand = function (b, a) {
    var c = this;
    b.collapsed = getOrgChart.EXPANDED;
    if ((b.parent == this._zu) || (b.parent == null)) {
        this.loadFromJSON(this.nodes, true, function () {
            c._V("updatedEvent");
            a()
        })
    } else {
        this.expand(b.parent, a)
    }
};
getOrgChart.prototype.expandOrCollapse = function (a) {
    var c = this;
    var b = this.nodes[a];
    this._a2 = {
        id: a,
        old_x: b.x,
        old_y: b.y
    };
    if (b.collapsed == getOrgChart.EXPANDED) {
        b.collapsed = getOrgChart.COLLAPSED
    } else {
        b.collapsed = getOrgChart.EXPANDED
    }
    this.loadFromJSON(this.nodes, true, function () {
        if (b.collapsed == getOrgChart.EXPANDED) {
            c.moveToMostDeepChildForNode(b)
        }
        c._V("updatedEvent")
    })
};
getOrgChart.prototype.moveToMostDeepChildForNode = function (b) {
    var c = getOrgChart.util._8(this._X);
    b = b.getMostDeepChild(this.nodes);
    var d = this.config.siblingSeparation / 2;
    var e = this.config.levelSeparation / 2;
    var a = c.slice(0);
    switch (this.config.orientation) {
        case getOrgChart.RO_TOP:
        case getOrgChart.RO_TOP_PARENT_LEFT:
            a[1] = ((b.y + b.h)) - c[3] + e;
            if (c[1] < a[1]) {
                this.move(a)
            }
            break;
        case getOrgChart.RO_BOTTOM:
        case getOrgChart.RO_BOTTOM_PARENT_LEFT:
            a[1] = b.y - e;
            if (c[1] > a[1]) {
                this.move(a)
            }
            break;
        case getOrgChart.RO_RIGHT:
        case getOrgChart.RO_RIGHT_PARENT_TOP:
            a[0] = b.x - d;
            if (c[0] > a[0]) {
                this.move(a)
            }
            break;
        case getOrgChart.RO_LEFT:
        case getOrgChart.RO_LEFT_PARENT_TOP:
            a[0] = ((b.x + b.w)) - c[2] + d;
            if (c[0] < a[0]) {
                this.move(a)
            }
            break
    }
};
getOrgChart.prototype.insertNode = function (e, b, c) {
    var a = {
        id: c,
        pid: e,
        data: b
    };
    if (!this._V("insertNodeEvent", a)) {
        return
    }
    b = a.data;
    e = a.pid;
    c = a.id;
    var g = this;
    var f = this.nodes[e];
    this._a2 = {
        id: e,
        old_x: f.x,
        old_y: f.y
    };
    if (c == undefined) {
        c = getOrgChart.util._G(this.nodes)
    }
    if (b == undefined || b == null) {
        b = {};
        this.config.primaryFields.forEach(function (h) {
            b[h] = h
        })
    }
    var d = this._h(c, e, null, b, false);
    d.x = f.x;
    d.y = f.y;
    this.loadFromJSON(this.nodes, true, function () {
        g.moveToMostDeepChildForNode(g.nodes[d.id]);
        g._V("updatedEvent")
    });
    return d
};
getOrgChart.prototype.removeNode = function (b) {
    var e = this;
    if (!this._V("removeNodeEvent", {
        id: b
    })) {
        return
    }
    var a = this.nodes[b];
    var d = a.parent;
    for (j = 0; j < a.children.length; j++) {
        a.children[j].pid = d.id
    }
    var c = this._X.getNodeById(b);
    c.parentNode.removeChild(c);
    delete this.nodes[b];
    this.loadFromJSON(this.nodes, true, function () {
        e._V("updatedEvent")
    })
};
getOrgChart.prototype.updateNode = function (b, d, a) {
    var e = this;
    var c = e.nodes[b];
    if (this._V("updateNodeEvent", {
        node: c,
        pid: d,
        data: a
    })) {
        c.pid = d;
        c.data = a;
        this.loadFromJSON(this.nodes, true, function () {
            e._V("updatedEvent")
        })
    }
};
getOrgChart.prototype._h = function (d, f, g, c, a, b) {
    var h = this.config.customize[d] && this.config.customize[d].theme ? getOrgChart.themes[this.config.customize[d].theme] : this.theme;
    a = (a == undefined ? getOrgChart.NOT_DEFINED : a);
    var e = new getOrgChart.node(d, f, g, c, h.size, this.config.photoFields, a, b);
    if (!this._V("createNodeEvent", {
        node: e
    })) {
        return null
    }
    if (this.nodes[d]) {
        e._z2 = this.nodes[d].x;
        e._z3 = this.nodes[d].y
    } else {
        e._z2 = null;
        e._z3 = null
    }
    this.nodes[d] = e;
    for (label in e.data) {
        if (!getOrgChart.util._s(this._ai, label)) {
            this._ai.push(label)
        }
    }
    return e
};
getOrgChart.prototype._aN = function (k, c) {
    c[0].stopPropagation();
    c[0].preventDefault();
    document.body.style.mozUserSelect = document.body.style.webkitUserSelect = document.body.style.userSelect = "none";
    var n = getOrgChart.util._8(this._X);
    var m = this;
    var l = this._X._v;
    var b = {
        x: c[0].clientX,
        y: c[0].clientY
    };
    var d = getOrgChart.util._6(k);
    var o = d[4];
    var p = d[5];
    var h = this.element.offsetWidth / n[2];
    var i = this.element.offsetHeight / n[3];
    var g = h > i ? i : h;
    var a = k.cloneNode(true);
    l.insertBefore(a, l.firstChild);
    a.style.opacity = 0.7;
    var f = function (q) {
        if (b) {
            var r = q.clientX;
            var s = q.clientY;
            var t = (r - b.x) / g;
            var u = (s - b.y) / g;
            d[4] = o + t;
            d[5] = p + u;
            a.setAttribute("transform", "matrix(" + d.toString() + ")")
        }
    };
    var e = function (q) {
        l.removeChild(a);
        l.removeEventListener("mousemove", f);
        l.removeEventListener("mouseup", e);
        l.removeEventListener("mouseleave", e);
        var r = k.getAttribute("data-node-id");
        if (m._ao = null && m._ao == r) {
            var s = m.nodes[r];
            var t = m.nodes[m._ak];
            if (!t._ay(s)) {
                m.updateNode(r, m._ak, s.data)
            }
        }
    };
    l.addEventListener("mousemove", f);
    l.addEventListener("mouseup", e);
    l.addEventListener("mouseleave", e)
};
getOrgChart.prototype._aJ = function (b, a) {
    if (!this.config.enableDragDrop) {
        return
    }
    this._ak = b.getAttribute("data-node-id")
};
getOrgChart.prototype._aU = function (b, a) {
    if (!this.config.enableDragDrop) {
        return
    }
    this._ak = null
};
getOrgChart.prototype._C = function (b, a) {
    this.exportToImage()
};
getOrgChart.prototype.exportToImage = function () {
    var d = document.getElementById("getExportChartDiv");
    if (!d) {
        d = document.createElement("div");
        d.style.display = "none";
        d.style.width = "1px";
        d.style.height = "1px";
        d.style.position = "absolute";
        d.style.top = "-10000px";
        d.style.left = "-10000px";
        d.id = "getExportChartDiv"
    }
    document.body.appendChild(d);
    var b = JSON.parse(JSON.stringify(this.config));
    b.scale = 1;
    b.expandToLevel = null;
    var o = new getOrgChart(d, b);
    o.loadFromJSON(this.nodes, true);
    var j = 0;
    var k = 0;
    var l = 0;
    var m = 0;
    for (var g in o.nodes) {
        var n = o.nodes[g];
        if (n.x > j) {
            j = n.x
        }
        if (n.y > k) {
            k = n.y
        }
        if (n.x < l) {
            l = n.x
        }
        if (n.y < m) {
            m = n.y
        }
    }
    width = j;
    height = k;
    if (width > 32767) {
        width = 32767
    }
    if (height > 32767) {
        height = 32767
    }
    var r = (l - this.config.subtreeSeparation) + "," + (m) + "," + (j + this.theme.size[0] + this.config.subtreeSeparation * 2) + "," + (k + this.theme.size[1]);
    o._X._v.setAttribute("viewBox", r);
    var q = "png";
    var a = document.getElementById("getExportChartCanvas");
    if (!a) {
        a = document.createElement("canvas");
        a.width = width;
        a.height = height;
        a.style.position = "absolute";
        a.style.top = "-10000px";
        a.style.left = "-10000px";
        a.id = "getExportChartCanvas";
        document.body.appendChild(a)
    }
    var c = a.getContext("2d");
    var h = d.getElementsByTagName("image");

    function p(t, s) {
        if (!t) {
            s()
        } else {
            var u = new XMLHttpRequest();
            u.onload = function () {
                var v = new FileReader();
                v.onloadend = function () {
                    t.href.baseVal = v.result;
                    s()
                };
                v.readAsDataURL(u.response)
            };
            u.open("GET", t.href.baseVal);
            u.responseType = "blob";
            u.send()
        }
    }

    function e(u, t, s) {
        p(u[t], function () {
            if (u.length > t - 1) {
                t++;
                var v = u[t];
                if (!v) {
                    s()
                } else {
                    e(u, t, s)
                }
            }
        })
    }

    function f() {
        function t(w) {
            var v = new MouseEvent("click", {
                view: window,
                bubbles: false,
                cancelable: true
            });
            var u = document.createElement("a");
            u.setAttribute("download", "getorgchart.png");
            u.setAttribute("href", w);
            u.setAttribute("target", "_blank");
            u.dispatchEvent(v)
        }
        var s = "";
        for (i = 0; i < document.styleSheets.length; i++) {
            if (document.styleSheets[i].href && document.styleSheets[i].href.indexOf("get-org-chart.css") != -1) {
                s = document.styleSheets[i].href;
                break
            }
            if (document.styleSheets[i].href && document.styleSheets[i].href.indexOf("getorgchart.css") != -1) {
                s = document.styleSheets[i].href;
                break
            }
        }
        get._a._B(s, null, function (u) {
            var v = (new XMLSerializer()).serializeToString(o._X._v);
            v = v.replace("<svg", '<svg width="' + width + '" height="' + height + '" ').replace("<defs/>", '<defs/><g class="' + o._X._zO.className + '"><g class="get-oc-tb"><g class="get-oc-c">').replace("</defs>", '</defs><g class="' + o._X._zO.className + '"><g class="get-oc-tb"><g class="get-oc-c">').replace("<defs/>", '<defs><style type="text/css">' + u + "</style></defs>").replace("</defs>", '<style type="text/css">' + u + "</style></defs>").replace("</svg>", "</g></g></g></svg>");
            var w = window.URL || window.webkitURL || window;
            var x = new Image();
            var y = new Blob([v], {
                type: "image/svg+xml;charset=utf-8"
            });
            var z = w.createObjectURL(y);
            x.onload = function () {
                c.drawImage(x, 0, 0);
                w.revokeObjectURL(z);
                var A = a.toDataURL("image/png").replace("image/png", "image/octet-stream");
                t(A)
            };
            x.src = z
        })
    }
    e(h, 0, function () {
        f()
    })
};
getOrgChart.prototype.load = function () {
    var a = this.config.dataSource;
    if (!a) {
        return
    }
    if (a.constructor && (a.constructor.toString().indexOf("HTML") > -1)) {
        this.loadFromHTMLTable(a)
    } else {
        if (typeof (a) == "string") {
            this.loadFromXML(a)
        } else {
            a = JSON.parse(JSON.stringify(this.config.dataSource));
            this.loadFromJSON(a)
        }
    }
};
getOrgChart.prototype.loadFromJSON = function (g, q, a) {
    this._zm = 0;
    this._zj = 0;
    this._zu = new getOrgChart.node(-1, null, null, null, 2, 2);
    if (q) {
        for (var e in g) {
            if (this.nodes[e] && !this.nodes[e].isVisible()) {
                this.nodes[e].x = this.nodes[e].parent.x;
                this.nodes[e].y = this.nodes[e].parent.y
            }
            this._h(e, g[e].pid, g[e].spid, g[e].data, g[e].collapsed, g[e].color)
        }
    } else {
        var f = Object.keys(g[0])[0];
        var m = Object.keys(g[0])[1];
        var s = null;
        if (this.config.idField) {
            f = this.config.idField
        }
        if (this.config.parentIdField) {
            m = this.config.parentIdField
        }
        if (this.config.secondParentIdField) {
            s = this.config.secondParentIdField
        }
        for (var d = 0; d < g.length; d++) {
            var e = g[d][f];
            var n = g[d][m];
            var t = null;
            delete g[d][f];
            delete g[d][m];
            if (s != null) {
                t = g[d][s];
                delete g[d][s]
            }
            this._h(e, n, t, g[d])
        }
    }
    for (var e in this.nodes) {
        var l = this.nodes[e];
        var o = this.nodes[l.pid];
        if (!o) {
            o = this._zu
        }
        l.parent = o;
        var c = o.children.length;
        o.children[c] = l;
        if (l.spid != null && l.spid != "") {
            var u = this.nodes[l.spid];
            l.secondParent = u;
            var p = u.secondChildren.length;
            u.secondChildren[p] = l
        }
    }
    if (this.config.layout == getOrgChart.MIXED_HIERARCHY_RIGHT_LINKS) {
        for (var e in this.nodes) {
            var l = this.nodes[e];
            var j = getOrgChart.util._ap(l);
            l.l = j;
            if (j > this.maxLevel) {
                this.maxLevel = j
            }
        }
        for (var e in this.nodes) {
            var l = this.nodes[e];
            if (l.l < this.maxLevel) {
                continue
            }
            var o = this.nodes[l.pid];
            var k = e;
            if (l.children.length == 0) {
                for (z = 0; z < o.children.length; z++) {
                    var b = o.children[z];
                    if (b.children.length == 0 && b != this.nodes[k] && b.displayPid == null && b.displayChildren == null) {
                        if (this.nodes[k].color == null) {
                            this.nodes[k].color = getOrgChart.util._4(this.config.color)
                        }
                        b.color = this.nodes[k].color;
                        b.displayPid = k;
                        b.displayParent = this.nodes[k];
                        if (this.nodes[k].displayChildren == null) {
                            this.nodes[k].displayChildren = []
                        }
                        var c = this.nodes[k].displayChildren.length;
                        this.nodes[k].displayChildren[c] = b;
                        k = b.id
                    }
                }
            }
        }
    }
    this.draw(a)
};
getOrgChart.prototype.loadFromHTMLTable = function (c) {
    var d = c.rows[0];
    var g = [];
    for (var e = 1; e < c.rows.length; e++) {
        var h = c.rows[e];
        var b = {};
        for (var f = 0; f < h.cells.length; f++) {
            var a = h.cells[f];
            b[d.cells[f].innerHTML] = a.innerHTML
        }
        g.push(b)
    }
    this.loadFromJSON(g)
};
getOrgChart.prototype.loadFromXML = function (c) {
    var a = this;
    var b = [];
    get._a._B(c, null, function (d) {
        a._aA = 0;
        a._aQ(d, null, true, b);
        a.loadFromJSON(b)
    }, "xml")
};
getOrgChart.prototype.loadFromXMLDocument = function (b) {
    var a = [];
    var c = getOrgChart.util._zG(b);
    this._aA = 0;
    this._aQ(c, null, true, a);
    this.loadFromJSON(a)
};
getOrgChart.prototype._aQ = function (m, l, d, h) {
    var a = this;
    if (m.nodeType == 1) {
        if (m.attributes.length > 0) {
            var c = {};
            a._aA++;
            c.id = a._aA;
            c.pid = l;
            for (var g = 0; g < m.attributes.length; g++) {
                var b = m.attributes.item(g);
                c[b.nodeName] = b.nodeValue
            }
            h.push(c);
            if (d) {
                d = false
            }
        }
    }
    if (m.hasChildNodes()) {
        if (!d) {
            l = a._aA
        }
        for (var e = 0; e < m.childNodes.length; e++) {
            var f = m.childNodes.item(e);
            var k = f.nodeName;
            if (f.nodeType == 3) {
                continue
            }
            this._aQ(f, l, d, h)
        }
    }
};
if (typeof (get) == "undefined") {
    get = {}
}
get._a = {};
get._a._z1 = function () {
    var a;
    if (window.XMLHttpRequest) {
        return new XMLHttpRequest()
    } else {
        return new ActiveXObject("Microsoft.XMLHTTP")
    }
};
get._a._zE = function (f, a, d, c, b, e) {
    var g = get._a._z1();
    g.open(d, f, e);
    g.onreadystatechange = function () {
        if (!get._c().msie && c == "xml" && g.readyState == 4) {
            a(g.responseXML)
        } else {
            if (get._c().msie && c == "xml" && g.readyState == 4) {
                try {
                    var i = new DOMParser();
                    var j = i.parseFromString(g.responseText, "text/xml");
                    a(j)
                } catch (h) {
                    var j = new ActiveXObject("Microsoft.XMLDOM");
                    j.loadXML(g.responseText);
                    a(j)
                }
            } else {
                if (g.readyState == 4) {
                    a(g.responseText)
                }
            }
        }
    };
    if (d == "POST") {
        g.setRequestHeader("Content-type", "application/x-www-form-urlencoded")
    }
    g.send(b)
};
get._a._B = function (g, b, a, c, f) {
    var e = [];
    for (var d in b) {
        e.push(encodeURIComponent(d) + "=" + encodeURIComponent(b[d]))
    }
    get._a._zE(g + "?" + e.join("&"), a, "GET", c, null, f)
};
get._a._zx = function (g, b, a, c, f) {
    var e = [];
    for (var d in b) {
        e.push(encodeURIComponent(d) + "=" + encodeURIComponent(b[d]))
    }
    get._a._zE(g, a, "POST", c, e.join("&"), f)
};
getOrgChart.prototype._y = function (d, b) {
    var a = b[0];
    var c = new Date().getTime();
    if (b[0].touches.length == 1) {
        var f = getOrgChart.util._8(this._X);
        this._a8.dragStart = {
            x: (b[0].touches[0].pageX - this._X._t.offsetLeft),
            y: (b[0].touches[0].pageY - this._X._t.offsetTop),
            viewBoxLeft: f[0],
            viewBoxTop: f[1]
        }
    }
    if (b[0].touches.length == 2) {
        this._a8.dragStart = null;
        this._p = null;
        this._za = getOrgChart.util._zq(this._X._v, a)
    }
};
getOrgChart.prototype._b = function (j, d) {
    var c = d[0];
    c.preventDefault();
    if (d[0].touches.length == 1) {
        this._X._n = undefined;
        if (this._a8.dragStart) {
            var g = (d[0].touches[0].pageX - this._X._t.offsetLeft);
            var h = (d[0].touches[0].pageY - this._X._t.offsetTop);
            this._X._t.style.cursor = "move";
            var l = getOrgChart.util._8(this._X);
            var m = l[2] / this._a0;
            var f = l[3] / this._a9;
            var a = m > f ? m : f;
            l[0] = -((g - this._a8.dragStart.x) * a) + this._a8.dragStart.viewBoxLeft;
            l[1] = -((h - this._a8.dragStart.y) * a) + this._a8.dragStart.viewBoxTop;
            l[0] = parseInt(l[0]);
            l[1] = parseInt(l[1]);
            this._X._v.setAttribute("viewBox", l.toString())
        }
    }
    if (d[0].touches.length == 2) {
        var b = getOrgChart.util._zq(this._X._v, d[0]);
        var k = 1 + ((b - this._za) / (this._za / 100)) / 100;
        var i = getOrgChart.util._zz(this._X._v, d[0]);
        this.zoom(k, null, i);
        this._za = b
    }
};
getOrgChart.prototype._g = function (d, b) {
    var a = b[0];
    var c = new Date().getTime()
};
getOrgChart.prototype._a1 = function (i, c) {
    var b = c[0];
    this._aH(i, c);
    this._aK(i, c);
    var h = new Date().getTime();
    var j = this;
    if (this._aM == null) {
        this._aM = {
            time: h
        };
        return
    }
    if (b.touches && b.touches.length && b.touches.length > 1) {
        this._aM = null;
        return
    }
    if (!this.config.enableZoomOnNodeDoubleClick) {
        this._aM = null;
        return
    }
    var k = h - this._aM.time;
    if ((k < 400) && (k > 0)) {
        this._a8.dragStart = null;
        var d = i.getAttribute("data-node-id");
        var g = this.nodes[d];
        var l = getOrgChart.util._8(this._X);
        var f = l.slice(0);
        if (!this._aM.viewBox || this._aM.id != d) {
            var f = getOrgChart.util._8(this._X);
            var a = f[2] / f[3];
            f[2] = g.w * 1.5;
            f[3] = f[2] / a;
            f[0] = (g.x - (g.w / 4));
            f[1] = (g.y - (f[3] / 2) + g.h / 2);
            this.move(f);
            if (this._aM.viewBox) {
                l = this._aM.viewBox
            }
            this._aM = {
                id: d,
                viewBox: l
            }
        } else {
            this.move(this._aM.viewBox);
            this._aM = null
        }
    }
    if (this._aM) {
        this._aM.time = h
    }
};
getOrgChart.prototype._aP = function (c, b) {
    var a = b[0];
    a.preventDefault()
};
getOrgChart.prototype._aL = function (c, b) {
    var a = b[0];
    if (a.touches && a.touches.length && a.touches.length == 0) {
        this._aM = null
    }
};