# LinqCore2.1JoinSelectsPerformanceComparison

Compares different linq and non-linq queries to join 2 lists

Example run for n = 100000 (Release settings, i7-4770@3.4GHz 4 Cores, 16Gb RAM ):
                            Join() Execution Time:                  25 ms, list size 6666
                           Hashset Execution Time:                  11 ms, list size 6666
                        Dictionary Execution Time:                  26 ms, list size 6666
       Where(IndexOf).AsParallel() Execution Time:                 216 ms, list size 6666
      Where(Contains).AsParallel() Execution Time:                 130 ms, list size 6666
                 FindAll(Contains) Execution Time:                 373 ms, list size 6666
                    Where(IndexOf) Execution Time:                 379 ms, list size 6666
                   Where(Contains) Execution Time:                 363 ms, list size 6666
                      Where(Count) Execution Time:                4699 ms, list size 6666
                        Where(Any) Execution Time:                4699 ms, list size 6666
                      Select(Find) Execution Time:                3154 ms, list size 6666
                     Select(First) Execution Time:                9161 ms, list size 6666
                     
                     
                     
                     
    Example run for 5x times elements n = 500000 (Release settings, i7-4770@3.4GHz 4 Cores, 16Gb RAM ):                
                            Join() Execution Time:                 111 ms, list size 33333
                           Hashset Execution Time:                  51 ms, list size 33333
                        Dictionary Execution Time:                 121 ms, list size 33333
       Where(IndexOf).AsParallel() Execution Time:                2490 ms, list size 33333
      Where(Contains).AsParallel() Execution Time:                2399 ms, list size 33333
                 FindAll(Contains) Execution Time:                8064 ms, list size 33333
                    Where(IndexOf) Execution Time:                8083 ms, list size 33333
                   Where(Contains) Execution Time:                8131 ms, list size 33333
                      Where(Count) Execution Time:              122751 ms, list size 33333
                        Where(Any) Execution Time:              122751 ms, list size 33333
                      Select(Find) Execution Time:              145329 ms, list size 33333
