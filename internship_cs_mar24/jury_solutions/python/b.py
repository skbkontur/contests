s = input()
n = len(s)
arr = [int(c) for c in s]
ans = n
for k in range(1, n + 1):
    if n % k:
        continue
    res = 0
    for i in range(n):
        res += (i // k % 2) != arr[i]
    ans = min(ans, res, n - res)
print(ans)