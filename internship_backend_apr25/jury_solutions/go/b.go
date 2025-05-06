package main

import (
    "bufio"
    "fmt"
    "os"
    "sort"
)

func main() {
    reader := bufio.NewReader(os.Stdin)

    var houses []int
    var heaters []int

    var n, m int
    fmt.Fscan(reader, &n)
    fmt.Fscan(reader, &m)
    houses = make([]int, n)
    for i := 0; i < n; i++ {
        fmt.Fscan(reader, &houses[i])
    }

    heaters = make([]int, m)
    for i := 0; i < m; i++ {
        fmt.Fscan(reader, &heaters[i])
    }

    minRadius := make([]int, n)
    for i := range minRadius {
        minRadius[i] = 1000000000
    }

    j, k := 0, 0

    sort.Ints(houses)
    sort.Ints(heaters)

    for j < n {
        d1 := max(houses[j]-heaters[k], -houses[j]+heaters[k])
        minRadius[j] = min(minRadius[j], d1)

        if k > 0 {
            d2 := max(houses[j]-heaters[k-1], -houses[j]+heaters[k-1])

            minRadius[j] = min(minRadius[j], d2)
        }

        if heaters[k] < houses[j] && k < m-1 {
            k++
        } else {
            j++
        }
    }

    maxRadius := minRadius[0]
    for _, radius := range minRadius {
        if radius > maxRadius {
            maxRadius = radius
        }
    }

    fmt.Println(maxRadius)
}
