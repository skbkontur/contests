n, k = map(int, input().split())
arr = list(map(int, input().split()))

l, zeros, sm, ans = 0, 0, 0, 0
for r in range(n):
    sm += arr[r]
    zeros += arr[r] == 0
    while sm > k or zeros > 1:
        zeros -= arr[l] == 0
        sm -= arr[l]
        l += 1
    if sm <= k and zeros < 2:
        ans += r - l + 1
print(ans)
