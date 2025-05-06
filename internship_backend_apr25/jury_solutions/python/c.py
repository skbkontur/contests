class State:
    new = 0
    open = 1
    close = 3

class Colour:
    def __init__(self):
        self.right_border = -1
        self.left_border = 1000000000
        self.state = State.new

def solve():
    m, n = map(int, input().split())
    colors_line = list(map(lambda x: int(x) - 1, input().split()))
    colours = dict()
    for i in range(m):
        colour_id = colors_line[i]
        if colours.get(colour_id) is None:
            colours[colour_id] = Colour()
        if i < colours[colour_id].left_border:
            colours[colour_id].left_border = i
        if i > colours[colour_id].right_border:
            colours[colour_id].right_border = i
    stack = []
    colours_ids = []
    for i in range(m):
        colour_id = colors_line[i]
        if colours[colour_id].state == State.new:
            stack.append(i)
            colours[colour_id].state = State.open
        elif colours[colour_id].state == State.open:
            while colors_line[stack[-1]] != colour_id:
                colours_ids.append(colors_line[stack[-1]])
                colours[colors_line[stack.pop()]].state = State.close
        elif colours[colour_id].state == State.close:
            print(-1)
            return

    while len(stack) > 0:
        colours_ids.append(colors_line[stack.pop()])

    print(len(colours_ids))
    colours_ids.reverse()
    for colour_id in colours_ids:
        print(f"{colour_id + 1} {colours[colour_id].left_border + 1} {colours[colour_id].right_border + 1}")

if __name__ == '__main__':
    solve()
