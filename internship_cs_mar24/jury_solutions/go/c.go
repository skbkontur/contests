package main

import (
	"bufio"
	"fmt"
	"os"
)

type rooks struct {
	Cols              map[int64]int64
	Rows              map[int64]int64
	AdjacentColsTotal int64
	AdjacentRowsTotal int64
	ColsCount         int64
	RowsCount         int64
	N                 int64
}

func (r *rooks) calcAdjacentRows(x int64) int64 {
	var diff int64 = 0
	if cnt := r.Rows[x-1]; cnt != 0 || x == 1 {
		diff++
	}
	if cnt := r.Rows[x+1]; cnt != 0 || x == r.N {
		diff++
	}

	return diff
}

func (r *rooks) calcAdjacentCols(y int64) int64 {
	var diff int64 = 0
	if cnt := r.Cols[y-1]; cnt != 0 || y == 1 {
		diff++
	}
	if cnt := r.Cols[y+1]; cnt != 0 || y == r.N {
		diff++
	}

	return diff
}

func calcDiff(r *rooks, x, y int64) int64 {
	var res int64 = 0

	if _, ok := r.Cols[y]; !ok {
		r.Cols[y] = 0
	}

	if _, ok := r.Rows[x]; !ok {
		r.Rows[x] = 0
	}

	adjacentRows := r.calcAdjacentRows(x)
	adjacentCols := r.calcAdjacentCols(y)

	// diff from new row intersecting cols
	if r.Rows[x] == 0 {
		switch adjacentRows {
		case 0:
			res += (r.ColsCount - r.AdjacentColsTotal + 1)
		case 2:
			res -= (r.ColsCount - r.AdjacentColsTotal + 1)
		}
	}

	// diff from new col intersecting rows (including the one we just added)
	if r.Cols[y] == 0 {
		if r.Rows[x] == 0 {
			switch adjacentCols {
			case 0:
				res += (r.RowsCount + 1 - (r.AdjacentRowsTotal + adjacentRows) + 1)
			case 2:
				res -= (r.RowsCount + 1 - (r.AdjacentRowsTotal + adjacentRows) + 1)
			}
		} else {
			switch adjacentCols {
			case 0:
				res += (r.RowsCount - r.AdjacentRowsTotal + 1)
			case 2:
				res -= (r.RowsCount - r.AdjacentRowsTotal + 1)
			}
		}
	}

	return res
}

func (r *rooks) updateAdd(x, y int64) {
	if r.Rows[x] == 0 {
		r.AdjacentRowsTotal += r.calcAdjacentRows(x)
		r.RowsCount++
	}
	if r.Cols[y] == 0 {
		r.AdjacentColsTotal += r.calcAdjacentCols(y)
		r.ColsCount++
	}
	r.Rows[x]++
	r.Cols[y]++
}

func (r *rooks) updateRemove(x, y int64) {
	if r.Rows[x] == 1 {
		r.AdjacentRowsTotal -= r.calcAdjacentRows(x)
		r.RowsCount--
	}
	if r.Cols[y] == 1 {
		r.AdjacentColsTotal -= r.calcAdjacentCols(y)
		r.ColsCount--
	}
	r.Rows[x]--
	r.Cols[y]--
}

func main() {

	in := bufio.NewReader(os.Stdin)

	var n, q int64

	fmt.Fscan(in, &n, &q)

	rooks := &rooks{
		Cols:              make(map[int64]int64, 0),
		Rows:              make(map[int64]int64, 0),
		AdjacentColsTotal: 0,
		AdjacentRowsTotal: 0,
		ColsCount:         0,
		RowsCount:         0,
		N:                 n,
	}

	var op string
	var x, y int64
	var curComp int64 = 1
	for i := int64(0); i < q; i++ {
		fmt.Fscan(in, &op, &x, &y)
		switch op {
		case "+":
			curComp += calcDiff(rooks, x, y)
			rooks.updateAdd(x, y)
		case "-":
			rooks.updateRemove(x, y)
			curComp -= calcDiff(rooks, x, y)
		}

		fmt.Println(curComp)
	}

}