from bisect import bisect_left

maxX, maxY = map(int, input().split())
xCount, yCount = map(int, input().split())
xLines = list(map(int, input().split()))
yLines = list(map(int, input().split()))
queriesCount = int(input())
xLines.append(0)
xLines.append(maxX)
yLines.append(0)
yLines.append(maxY)
xLines.sort()
yLines.sort()
for _ in range(queriesCount):
    x1, y1, x2, y2 = map(int, input().split())
    diffX = bisect_left(xLines, x1) == bisect_left(xLines, x2)
    diffY = bisect_left(yLines, y1) == bisect_left(yLines, y2)
    if diffX and diffY:
        print("YES")
    else:
        print("NO")
