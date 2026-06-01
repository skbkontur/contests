function main() {
    var line = readline().trim();
    var parts = line.split(' ');
    var N = parseInt(parts[0]);
    var M = parseInt(parts[1]);
    var s = readline().trim();
    var pairCount = Array(26);
    for (var i = 0; i < 26; i++) {
        pairCount[i] = Array(26);
        for (var j = 0; j < 26; j++) pairCount[i][j] = 0;
    }
    for (var k = 0; k < M; k++) {
        var p = readline().trim();
        var aChar = p[0];
        var bChar = p[1];
        if (p[1] === ' ') {
            bChar = p[2];
        }
        var a = aChar.charCodeAt(0) - 97;
        var b = bChar.charCodeAt(0) - 97;
        pairCount[a][b]++;
    }
    var suf = Array(26);
    for (var i = 0; i < 26; i++) suf[i] = 0;
    var ans = 0;
    for (var i = N - 1; i >= 0; i--) {
        var cur = s.charCodeAt(i) - 97;
        var row = pairCount[cur];
        for (var b = 0; b < 26; b++) {
            if (row[b] !== 0) {
                ans += suf[b] * row[b];
            }
        }
        suf[cur]++;
    }
    print(ans);
}

main();