import bisect
import sys

def make_move(direction, distance):
    global current_x
    global current_y
    new_x = current_x + distance * dx[direction]
    new_y = current_y + distance * dy[direction]

    if new_x == current_x and check_intersection(current_x, current_y, new_y, points_x):
        return False
    if new_y == current_y and check_intersection(current_y, current_x, new_x, points_y):
        return False

    current_x = new_x
    current_y = new_y
    return True

def check_intersection(key, start, finish, points):
    if key not in points:
        return False
    lst = points[key]

    if start > finish:
        start, finish = finish, start

    lower_bound = bisect.bisect_left(lst, start)

    return lower_bound < len(lst) and lst[lower_bound] <= finish


dx = {'U': 0, 'D': 0, 'L': -1, 'R': 1}
dy = {'U': 1, 'D': -1, 'L': 0, 'R': 0}
points_x = {}
points_y = {}

n = int(input())
for _ in range(n):
    ints = list(map(int, sys.stdin.readline().split()))
    x = ints[0]
    y = ints[1]
    if x not in points_x:
        points_x[x] = []
    points_x[x].append(y)
    if y not in points_y:
        points_y[y] = []
    points_y[y].append(x)

for x in points_x:
    points_x[x].sort()
for y in points_y:
    points_y[y].sort()

current_x = 0
current_y = 0

q = int(input())
for _ in range(1, q + 1):
    tokens = sys.stdin.readline().split()
    direction = tokens[0][0]
    distance = int(tokens[1])
    if not make_move(direction, distance):
        print("Stop", _)
        exit()

print("Complete")
