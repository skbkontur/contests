n = int(input())
f = list(map(int, input().split()))
mx, mn = 0, 0;
for i in range(n):
    if f[i] >= f[mx]:
        mx = i
    if f[i] < f[mn]:
        mn = i

print(mx + 1, mn + 1)
