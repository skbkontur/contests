ans = 0

def rec(last = 0, cur = 0):
	if cur in h[last]: return
	h[last].add(cur)
	if last == n:
	    global ans
	    ans += 1
	    return
	for val in f[last]:
		rec(last + 1, cur + val)

n = int(input())
h = [set() for _ in range(n + 1)]
f = [list(map(int, input().split())) for _ in range(n)]
rec()
print(ans)
