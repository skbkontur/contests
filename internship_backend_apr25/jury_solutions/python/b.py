def main():
    _ = input()  # Игнорируем первую строку ввода
    houses = list(map(int, input().split()))
    heaters = list(map(int, input().split()))

    min_radius = [float('inf')] * len(houses)

    j = 0
    k = 0

    houses.sort()
    heaters.sort()

    while j < len(houses):
        min_radius[j] = min(min_radius[j], abs(houses[j] - heaters[k]))

        if k > 0:
            min_radius[j] = min(min_radius[j], abs(houses[j] - heaters[k - 1]))

        if heaters[k] < houses[j] and k < len(heaters) - 1:
            k += 1
        else:
            j += 1

    print(max(min_radius))

if __name__ == "__main__":
    main()