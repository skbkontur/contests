import sys
 
input = sys.stdin.readline

def check(n, k, border):
	order = []
	return check_graph(n, border, order) or check_k_len_path(n, k, border, order)

def check_graph(n, border, order):
	colors = [0] * n
	res = False
	color = 1
	for i in range(n):
		if colors[i] != 0:
			continue
		res = res or top_sort_no_rec(i, border, colors, order, color)
		color += 1

	return res

def top_sort_no_rec(v, border, colors, order, cur_color):
	dfs = [(False, v)]
	while dfs:
		node = dfs.pop()
		if node[0]:
			order.append(node[1])
			colors[node[1]] = cur_color
			continue

		if colors[node[1]] != 0:
			continue

		colors[node[1]] = -1
		dfs.append((True, node[1]))

		for u in graph[node[1]]:
			if u[1] > border:
				continue

			if colors[u[0]] == -1:
				return True

			if colors[u[0]] == 0:
				dfs.append((False, u[0]))

	return False

def check_k_len_path(n, k, border, order):
	dp = [0] * n

	for v in order:
		dp[v] = 1

		for p in graph[v]:
			u = p[0]
			if p[1] <= border and dp[v] < dp[u] + 1:
				dp[v] = dp[u] + 1
			
		if dp[v] >= k:
			return True

	return False


if __name__ == '__main__':
	nmk = input().split(' ')
	n = int(nmk[0])
	m = int(nmk[1])
	k = int(nmk[2]) + 1
	k = min(k, n + 1)
	graph = [[] for _ in range(n)]
	mx = 0
	for _ in range(m):
		uvc = input().split(' ')
		u = int(uvc[0]) - 1
		v = int(uvc[1]) - 1
		c = int(uvc[2])
		graph[u].append((v, c))
		mx = max(mx, c)

	l = 1
	r = mx + 2
	ans = -1

	while l <= r:
		mid = (l + r) // 2

		if check(n, k, mid):
			r = mid - 1
			ans = mid
		else:
			l = mid + 1

	print(ans)
