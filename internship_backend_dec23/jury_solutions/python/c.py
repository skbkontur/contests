n, m = [int(x) for x in input().split()]
t = int(input())

rows = [(-1, 0)] * n
columns = [(-1, 0)] * m

for i in range(t):
    x, y, c = [int(x) for x in input().split()]
    rows[x - 1] = (i, c)
    columns[y - 1] = (i, c)

for i in range(n):
    list = []
    for j in range(m):
        if rows[i][0] < columns[j][0]:
            list.append(str(columns[j][1]))
        else:
            list.append(str(rows[i][1]))
    print(' '.join(list))
