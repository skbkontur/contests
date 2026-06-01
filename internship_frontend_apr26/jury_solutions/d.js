const MaxN = 100000;
const A = new Int32Array(MaxN);
const B = new Int32Array(MaxN);
const C = new Int32Array(MaxN);
const D = new Int32Array(MaxN);

function SqrtProduct(n, p) {
    this.p = p;
    this.len = Math.ceil(Math.sqrt(n));
    var len = this.len;
    var blockBegin = new Int32Array(len);
    var blockEnd = new Int32Array(len);
    var blockProduct = new Array(len);

    this.temp = new Int32Array(4);
    this.result = new Int32Array(4);

    for (var block = 0; block < len; block++) {
        var begin = block * len;
        var end = Math.min(begin + len - 1, n - 1);
        blockBegin[block] = begin;
        blockEnd[block] = end;

        var prod = new Int32Array([1, 0, 0, 1]);
        var t = this.temp;
        for (var i = begin; i <= end; i++) {
            var a = (prod[0] * A[i] + prod[1] * C[i]) % p;
            var b = (prod[0] * B[i] + prod[1] * D[i]) % p;
            var c = (prod[2] * A[i] + prod[3] * C[i]) % p;
            var d = (prod[2] * B[i] + prod[3] * D[i]) % p;
            prod[0] = a; prod[1] = b; prod[2] = c; prod[3] = d;
        }
        blockProduct[block] = prod;
    }
    this.blockBegin = blockBegin;
    this.blockEnd = blockEnd;
    this.blockProduct = blockProduct;
}

SqrtProduct.prototype.getProduct = function(left, right) {
    var p = this.p;
    var blockBegin = this.blockBegin;
    var blockEnd = this.blockEnd;
    var blockProduct = this.blockProduct;
    var len = this.len;
    var prod = this.result;
    prod[0] = 1; prod[1] = 0; prod[2] = 0; prod[3] = 1;
    var t = this.temp;

    var startBlock = (left / len) | 0;
    var endBlock = (right / len) | 0;
    for (var block = startBlock; block <= endBlock; block++) {
        var bl = blockBegin[block];
        var br = blockEnd[block];
        if (left <= bl && br <= right) {
            var bp = blockProduct[block];
            var a = (prod[0] * bp[0] + prod[1] * bp[2]) % p;
            var b = (prod[0] * bp[1] + prod[1] * bp[3]) % p;
            var c = (prod[2] * bp[0] + prod[3] * bp[2]) % p;
            var d = (prod[2] * bp[1] + prod[3] * bp[3]) % p;
            prod[0] = a; prod[1] = b; prod[2] = c; prod[3] = d;
        } else {
            var start = left > bl ? left : bl;
            var end = right < br ? right : br;
            for (var i = start; i <= end; i++) {
                var a = (prod[0] * A[i] + prod[1] * C[i]) % p;
                var b = (prod[0] * B[i] + prod[1] * D[i]) % p;
                var c = (prod[2] * A[i] + prod[3] * C[i]) % p;
                var d = (prod[2] * B[i] + prod[3] * D[i]) % p;
                prod[0] = a; prod[1] = b; prod[2] = c; prod[3] = d;
            }
        }
    }
    return prod;
};

function main() {
    var p = parseInt(readline());
    var nm = readline().split(' ');
    var n = parseInt(nm[0]), m = parseInt(nm[1]);

    for (var i = 0; i < n; i++) {
        var nums1 = readline().split(' ');
        var nums2 = readline().split(' ');
        A[i] = parseInt(nums1[0]);
        B[i] = parseInt(nums1[1]);
        C[i] = parseInt(nums2[0]);
        D[i] = parseInt(nums2[1]);
        readline();
    }

    var sqrt = new SqrtProduct(n, p);
    var firstAnswer = true;
    for (var q = 0; q < m; q++) {
        var lr = readline().split(' ');
        var l = parseInt(lr[0]), r = parseInt(lr[1]);
        var res = sqrt.getProduct(l - 1, r - 1);
        if (!firstAnswer) print('');
        firstAnswer = false;
        print(res[0] + ' ' + res[1]);
        print(res[2] + ' ' + res[3]);
    }
}

main();