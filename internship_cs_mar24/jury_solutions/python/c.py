n, q = map(int, input().split())
rows, cols = [0] * (n + 2), [0] * (n + 2)
rows[0] = cols[0] = rows[-1] = cols[-1] = 1
v, h = 2, 2
for _ in range(q):
    typ, x, y = input().split()
    x, y = int(x), int(y)
    if typ == '+':
        if not rows[x]:
            if rows[x - 1] and rows[x + 1]:
                h -= 1
            elif not rows[x - 1] and not rows[x + 1]:
                h += 1
        if not cols[y]:
            if cols[y - 1] and cols[y + 1]:
                v -= 1
            elif not cols[y - 1] and not cols[y + 1]:
                v += 1
        rows[x] += 1
        cols[y] += 1
    else:
        rows[x] -= 1
        cols[y] -= 1
        if not rows[x]:
            if rows[x - 1] and rows[x + 1]:
                h += 1
            elif not rows[x - 1] and not rows[x + 1]:
                h -= 1
        if not cols[y]:
            if cols[y - 1] and cols[y + 1]:
                v += 1
            elif not cols[y - 1] and not cols[y + 1]:
                v -= 1
    print((h - 1) * (v - 1))
