n = int(input())
f = [tuple(map(int, input().split())) for _ in range(n)]
s = set(f)
ans = 0
for i in range(n):
    for j in range(i + 1, n):
        area = abs(f[i][0] - f[j][0]) * abs(f[i][1] - f[j][1])
        if area <= ans: continue
        if (f[i][0], f[j][1]) in s and (f[j][0], f[i][1]) in s:
            ans = area
print(ans)
