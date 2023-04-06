n, m = map(int, input().split())
f = [[0] * (m + 2) for _ in range(n + 2)]
for i in range(1, n + 1):
    f[i][1:m+1] = list(map(int, input()))
ul = [[0] * (m + 2) for _ in range(n + 2)]
ur = [[0] * (m + 2) for _ in range(n + 2)]
dl = [[0] * (m + 2) for _ in range(n + 2)]
dr = [[0] * (m + 2) for _ in range(n + 2)]

for i in range(1, n + 1): 
    for j in range(1, m + 1):
	    ul[i][j] = max(f[i][j], ul[i - 1][j], ul[i][j - 1])
for i in range(1, n + 1): 
    for j in range(m, 0, -1):
	    ur[i][j] = max(f[i][j], ur[i - 1][j], ur[i][j + 1])
for i in range(n, 0, -1): 
    for j in range(1, m + 1):
	    dl[i][j] = max(f[i][j], dl[i + 1][j], dl[i][j - 1])
for i in range(n, 0, -1): 
    for j in range(m, 0, -1):
	    dr[i][j] = max(f[i][j], dr[i + 1][j], dr[i][j + 1])
ans = 10**9
for i in range(1, n + 1):
	for j in range(1, m + 1):
	    cur_max = max(ul[i - 1][j - 1], ur[i - 1][j + 1], dl[i + 1][j - 1], dr[i + 1][j + 1])
	    if ans > cur_max:
	        ans = cur_max
	        ans_x, ans_y = i, j
print(ans_x, ans_y)