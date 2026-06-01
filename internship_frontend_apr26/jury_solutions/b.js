function main() {
    var s = readline().trim();
    var n = s.length;
    var totalDelta = 0;
    for (var i = 0; i < n; ++i) {
        if (s[i] === '1') {
            if ((i + 1) % 2 === 1) totalDelta += 1;
            else totalDelta -= 1;
        }
    }
    var leftDelta = 0;
    for (var i = 0; i < n; ++i) {
        var delta = 0;
        if (s[i] === '1') {
            if ((i + 1) % 2 === 1) delta = 1;
            else delta = -1;
        }
        var diff = totalDelta - delta;
        if (diff % 2 === 0 && leftDelta === diff / 2) {
            print(i + 1);
            return;
        }
        leftDelta += delta;
    }
    print(0);
}

main();