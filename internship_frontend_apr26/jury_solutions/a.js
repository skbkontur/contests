function main() {
    var line = readline().trim();
    var parts = line.split(' ');
    var w = parseInt(parts[0]);
    var h = parseInt(parts[1]);
    
    var t1 = [];
    for (var i = 0; i < h; i++) {
        t1.push(readline().trim());
    }
    var t2 = [];
    for (var i = 0; i < h; i++) {
        t2.push(readline().trim());
    }
    var op = readline().trim();
    
    var res = [];
    for (var i = 0; i < h; i++) {
        var row = '';
        for (var j = 0; j < w; j++) {
            var a = t1[i][j];
            var b = t2[i][j];
            var idx = (a === '0' ? 0 : 2) + (b === '0' ? 0 : 1);
            row += op[idx];
        }
        res.push(row);
    }
    for (var i = 0; i < h; i++) {
        print(res[i]);
    }
}

main();