n1, n2 = map(int, input().split())

common = min(n1, n2)
ans = 3 * common
n1 -= common
n2 -= common

if n2 > 0:
    ans += 2
elif n1 > 0:
    ans += 1

print(ans)
