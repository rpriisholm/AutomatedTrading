namespace ValueCollections
{
    public static class PermanentValues
    {
        /* Syntax Small Indicator - Large Indicator - Lose Limit (positive like 0.01)*/
        #region TestPairs/All 
        public static readonly string TestPairs = @"SMA 10 - EMA 10 - 0.11
SMA 8 - EMA 10 - 0.11
HMA 12 - EMA 12 - 0.06
HMA 6 - EMA 12 - 0.07
SMMA 8 - EMA 12 - 0.09
SMMA 12 - EMA 14 - 0.11
HMA 10 - EMA 16 - 0.03
HMA 6 - EMA 16 - 0.07
SMA 14 - EMA 16 - 0.06
SMMA 14 - EMA 16 - 0.11
HMA 12 - EMA 18 - 0.03
HMA 8 - EMA 18 - 0.04
KAMA 2 - EMA 2 - 0.09
SMMA 2 - EMA 2 - 0.03
HMA 10 - EMA 20 - 0.03
SMA 16 - EMA 20 - 0.05
SMA 18 - EMA 22 - 0.11
HMA 10 - EMA 24 - 0.03
SMA 22 - EMA 24 - 0.00
SMA 26 - EMA 28 - 0.01
SMA 28 - EMA 28 - 0.10
HMA 12 - EMA 32 - 0.03
HMA 10 - EMA 36 - 0.03
HMA 12 - EMA 38 - 0.03
HMA 10 - EMA 44 - 0.03
LinearReg 6 - Highest 10 - 0.01
LinearReg 8 - Highest 10 - 0.02
LinearReg 12 - Highest 12 - 0.02
LinearReg 6 - Highest 12 - 0.01
LinearReg 8 - Highest 12 - 0.02
LinearReg 12 - Highest 14 - 0.02
LinearReg 8 - Highest 14 - 0.03
LinearReg 12 - Highest 16 - 0.02
LinearReg 14 - Highest 20 - 0.02
LinearReg 20 - Highest 22 - 0.03
LinearReg 14 - Highest 24 - 0.02
LinearReg 20 - Highest 24 - 0.03
LinearReg 22 - Highest 24 - 0.03
LinearReg 20 - Highest 26 - 0.02
LinearReg 26 - Highest 26 - 0.02
LinearReg 28 - Highest 28 - 0.02
LinearReg 20 - Highest 30 - 0.02
LinearReg 22 - Highest 30 - 0.03
LinearReg 26 - Highest 30 - 0.02
LinearReg 30 - Highest 30 - 0.02
LinearReg 28 - Highest 32 - 0.02
LinearReg 30 - Highest 32 - 0.01
LinearReg 32 - Highest 32 - 0.03
KAMA 10 - EMA 10 - 0.11
KAMA 8 - EMA 10 - 0.11
KAMA 10 - EMA 12 - 0.11
KAMA 12 - EMA 12 - 0.10
SMMA 10 - EMA 12 - 0.11
SMMA 12 - EMA 12 - 0.11
HMA 6 - EMA 14 - 0.09
SMA 14 - EMA 14 - 0.06
SMMA 14 - EMA 14 - 0.11
SMMA 12 - EMA 16 - 0.11
SMA 14 - EMA 18 - 0.05
SMA 16 - EMA 18 - 0.07
SMA 18 - EMA 18 - 0.08
SMMA 10 - EMA 18 - 0.11
Highest 2 - EMA 2 - 0.11
HMA 8 - EMA 20 - 0.04
HMA 12 - EMA 22 - 0.05
SMA 20 - EMA 22 - 0.11
SMA 22 - EMA 22 - 0.00
HMA 10 - EMA 26 - 0.03
SMA 22 - EMA 26 - 0.11
HMA 10 - EMA 30 - 0.03
HMA 10 - EMA 34 - 0.03
HMA 10 - EMA 42 - 0.03
HMA 12 - EMA 42 - 0.03
HMA 10 - EMA 46 - 0.03
KAMA 6 - EMA 6 - 0.11
SMA 6 - EMA 6 - 0.11
SMMA 4 - EMA 6 - 0.02
KAMA 6 - EMA 8 - 0.11
SMMA 8 - EMA 8 - 0.09
LinearReg 14 - Highest 16 - 0.02
LinearReg 10 - Highest 18 - 0.02
LinearReg 16 - Highest 18 - 0.02
LinearReg 8 - Highest 18 - 0.03
KAMA 2 - Highest 2 - 0.11
LinearReg 10 - Highest 20 - 0.02
LinearReg 16 - Highest 20 - 0.02
LinearReg 10 - Highest 22 - 0.02
LinearReg 16 - Highest 22 - 0.02
LinearReg 10 - Highest 24 - 0.01
LinearReg 16 - Highest 24 - 0.02
LinearReg 24 - Highest 24 - 0.03
LinearReg 8 - Highest 24 - 0.02
LinearReg 10 - Highest 26 - 0.01
LinearReg 16 - Highest 26 - 0.02
LinearReg 24 - Highest 26 - 0.02
LinearReg 8 - Highest 26 - 0.00
LinearReg 12 - Highest 28 - 0.02
HMA 10 - EMA 10 - 0.10
KAMA 6 - EMA 10 - 0.11
SMMA 10 - EMA 10 - 0.09
SMMA 8 - EMA 10 - 0.09
HMA 10 - EMA 12 - 0.04
SMA 10 - EMA 12 - 0.07
HMA 12 - EMA 14 - 0.03
SMMA 10 - EMA 14 - 0.11
SMMA 8 - EMA 14 - 0.10
HMA 8 - EMA 16 - 0.09
SMA 12 - EMA 16 - 0.00
HMA 10 - EMA 18 - 0.03
SMMA 12 - EMA 18 - 0.11
SMA 2 - EMA 2 - 0.11
SMA 20 - EMA 20 - 0.00
SMA 20 - EMA 24 - 0.04
HMA 12 - EMA 26 - 0.03
SMA 24 - EMA 26 - 0.00
SMA 26 - EMA 26 - 0.07
HMA 12 - EMA 30 - 0.03
HMA 10 - EMA 32 - 0.03
HMA 12 - EMA 34 - 0.03
HMA 10 - EMA 38 - 0.03
KAMA 2 - EMA 4 - 0.11
HMA 12 - EMA 46 - 0.03
KAMA 4 - EMA 6 - 0.11
SMA 4 - EMA 6 - 0.11
SMA 6 - EMA 8 - 0.11
SMMA 6 - EMA 8 - 0.10
LinearReg 10 - Highest 10 - 0.02
LinearReg 10 - Highest 12 - 0.02
LinearReg 10 - Highest 14 - 0.02
LinearReg 6 - Highest 14 - 0.01
LinearReg 10 - Highest 16 - 0.02
LinearReg 16 - Highest 16 - 0.02
LinearReg 8 - Highest 16 - 0.03
LinearReg 12 - Highest 18 - 0.02
LinearReg 18 - Highest 18 - 0.02
LinearReg 12 - Highest 20 - 0.02
LinearReg 18 - Highest 20 - 0.02
LinearReg 8 - Highest 20 - 0.03
LinearReg 12 - Highest 22 - 0.02
LinearReg 18 - Highest 22 - 0.02
LinearReg 8 - Highest 22 - 0.02
LinearReg 12 - Highest 24 - 0.02
LinearReg 18 - Highest 24 - 0.02
LinearReg 12 - Highest 26 - 0.02
LinearReg 18 - Highest 26 - 0.02
LinearReg 14 - Highest 28 - 0.02
HMA 6 - EMA 10 - 0.10
SMMA 6 - EMA 10 - 0.10
KAMA 12 - EMA 14 - 0.10
SMA 12 - EMA 14 - 0.07
HMA 12 - EMA 16 - 0.03
SMMA 10 - EMA 16 - 0.11
SMMA 14 - EMA 18 - 0.11
HMA 12 - EMA 20 - 0.04
SMA 18 - EMA 20 - 0.07
HMA 10 - EMA 22 - 0.03
HMA 8 - EMA 22 - 0.11
HMA 12 - EMA 24 - 0.03
SMA 24 - EMA 24 - 0.11
HMA 10 - EMA 28 - 0.02
HMA 12 - EMA 28 - 0.03
HMA 12 - EMA 36 - 0.03
HMA 10 - EMA 40 - 0.03
HMA 12 - EMA 40 - 0.03
HMA 12 - EMA 44 - 0.03
SMMA 6 - EMA 6 - 0.11
LinearReg 14 - Highest 14 - 0.05
LinearReg 14 - Highest 18 - 0.02
LinearReg 20 - Highest 20 - 0.02
LinearReg 14 - Highest 22 - 0.02
LinearReg 22 - Highest 22 - 0.03
LinearReg 14 - Highest 26 - 0.02
LinearReg 22 - Highest 26 - 0.03
LinearReg 10 - Highest 28 - 0.01
LinearReg 16 - Highest 28 - 0.02
LinearReg 24 - Highest 28 - 0.02
LinearReg 8 - Highest 28 - 0.00
LinearReg 10 - Highest 30 - 0.01
LinearReg 16 - Highest 30 - 0.03
LinearReg 28 - Highest 30 - 0.02
LinearReg 10 - Highest 32 - 0.01
LinearReg 16 - Highest 32 - 0.02
LinearReg 24 - Highest 32 - 0.02
LinearReg 10 - Highest 34 - 0.01
LinearReg 12 - Highest 34 - 0.02
LinearReg 16 - Highest 34 - 0.02
LinearReg 24 - Highest 34 - 0.02
LinearReg 30 - Highest 34 - 0.01
LinearReg 8 - Highest 34 - 0.02
LinearReg 10 - Highest 36 - 0.01
LinearReg 16 - Highest 36 - 0.02
LinearReg 24 - Highest 36 - 0.02
LinearReg 8 - Highest 36 - 0.02
LinearReg 12 - Highest 38 - 0.02
LinearReg 20 - Highest 28 - 0.02
LinearReg 22 - Highest 28 - 0.03
LinearReg 26 - Highest 28 - 0.02
LinearReg 14 - Highest 30 - 0.02
LinearReg 14 - Highest 32 - 0.02
LinearReg 20 - Highest 32 - 0.02
LinearReg 22 - Highest 32 - 0.03
LinearReg 26 - Highest 32 - 0.02
LinearReg 14 - Highest 34 - 0.02
LinearReg 22 - Highest 34 - 0.02
LinearReg 28 - Highest 34 - 0.02
LinearReg 20 - Highest 36 - 0.02
LinearReg 26 - Highest 36 - 0.02
LinearReg 32 - Highest 36 - 0.03
LinearReg 28 - Highest 38 - 0.02
LinearReg 34 - Highest 38 - 0.04
LinearReg 20 - Highest 40 - 0.03
LinearReg 22 - Highest 40 - 0.02
LinearReg 26 - Highest 40 - 0.03
LinearReg 28 - Highest 40 - 0.02
LinearReg 34 - Highest 40 - 0.04
LinearReg 28 - Highest 42 - 0.02
LinearReg 34 - Highest 42 - 0.04
LinearReg 42 - Highest 42 - 0.06
LinearReg 34 - Highest 44 - 0.04
LinearReg 42 - Highest 44 - 0.06
LinearReg 28 - Highest 46 - 0.02
LinearReg 34 - Highest 46 - 0.04
LinearReg 42 - Highest 46 - 0.06
EMA 10 - HMA 10 - 0.11
EMA 8 - HMA 10 - 0.10
KAMA 4 - HMA 10 - 0.10
KAMA 6 - HMA 10 - 0.11
SMMA 10 - HMA 10 - 0.11
SMMA 8 - HMA 10 - 0.11
EMA 10 - HMA 12 - 0.10
Highest 12 - HMA 12 - 0.11
Highest 6 - HMA 12 - 0.11
KAMA 6 - HMA 12 - 0.10
SMMA 10 - HMA 12 - 0.11
SMMA 12 - HMA 12 - 0.08
SMMA 8 - HMA 12 - 0.11
Highest 6 - HMA 14 - 0.11
Highest 8 - HMA 14 - 0.11
KAMA 12 - HMA 14 - 0.11
KAMA 14 - HMA 14 - 0.11
KAMA 8 - HMA 14 - 0.11
SMA 10 - HMA 14 - 0.11
Highest 6 - HMA 16 - 0.11
KAMA 10 - HMA 16 - 0.11
KAMA 12 - HMA 16 - 0.11
SMA 10 - HMA 16 - 0.11
SMMA 12 - HMA 16 - 0.11
SMMA 16 - HMA 16 - 0.09
EMA 18 - HMA 18 - 0.09
SMA 12 - HMA 18 - 0.07
SMMA 14 - HMA 18 - 0.08
SMMA 6 - HMA 18 - 0.11
KAMA 2 - HMA 2 - 0.05
LinearReg 2 - HMA 2 - 0.11
Lowest 2 - HMA 2 - 0.11
SMA 2 - HMA 2 - 0.11
EMA 20 - HMA 20 - 0.09
Highest 4 - HMA 20 - 0.11
Highest 6 - HMA 20 - 0.11
JMA 12 - HMA 20 - 0.11
JMA 14 - HMA 20 - 0.11
JMA 16 - HMA 20 - 0.11
JMA 18 - HMA 20 - 0.11
KAMA 10 - HMA 20 - 0.11
KAMA 18 - HMA 20 - 0.11
SMA 10 - HMA 20 - 0.11
SMA 20 - HMA 20 - 0.09
SMMA 16 - HMA 20 - 0.10
SMMA 20 - HMA 20 - 0.11
JMA 10 - HMA 22 - 0.11
JMA 20 - HMA 22 - 0.11
KAMA 12 - HMA 22 - 0.11
KAMA 14 - HMA 22 - 0.08
KAMA 8 - HMA 22 - 0.11
SMA 10 - HMA 22 - 0.11
SMA 20 - HMA 22 - 0.09
SMMA 16 - HMA 22 - 0.10
SMMA 20 - HMA 22 - 0.11
EMA 14 - HMA 24 - 0.11
EMA 16 - HMA 24 - 0.11
KAMA 20 - HMA 24 - 0.11
KAMA 22 - HMA 24 - 0.11
KAMA 24 - HMA 24 - 0.11
SMA 14 - HMA 24 - 0.09
SMA 22 - HMA 24 - 0.11
SMA 6 - HMA 24 - 0.11
SMA 8 - HMA 24 - 0.11
SMMA 14 - HMA 24 - 0.11
SMMA 18 - HMA 24 - 0.11
SMMA 4 - HMA 24 - 0.11
SMMA 6 - HMA 24 - 0.11
EMA 18 - HMA 26 - 0.11
LinearReg 32 - Highest 34 - 0.03
LinearReg 14 - Highest 36 - 0.02
LinearReg 22 - Highest 36 - 0.02
LinearReg 28 - Highest 36 - 0.02
LinearReg 30 - Highest 36 - 0.02
LinearReg 36 - Highest 36 - 0.04
LinearReg 10 - Highest 38 - 0.01
LinearReg 16 - Highest 38 - 0.02
LinearReg 24 - Highest 38 - 0.03
LinearReg 8 - Highest 38 - 0.02
LinearReg 10 - Highest 40 - 0.01
LinearReg 16 - Highest 40 - 0.02
LinearReg 40 - Highest 40 - 0.04
LinearReg 10 - Highest 42 - 0.02
LinearReg 16 - Highest 42 - 0.02
LinearReg 24 - Highest 42 - 0.03
LinearReg 38 - Highest 42 - 0.04
LinearReg 40 - Highest 42 - 0.04
LinearReg 10 - Highest 44 - 0.01
LinearReg 12 - Highest 44 - 0.02
LinearReg 16 - Highest 44 - 0.02
LinearReg 24 - Highest 44 - 0.03
LinearReg 40 - Highest 44 - 0.04
LinearReg 8 - Highest 44 - 0.02
LinearReg 10 - Highest 46 - 0.01
LinearReg 16 - Highest 46 - 0.02
LinearReg 24 - Highest 46 - 0.03
LinearReg 38 - Highest 46 - 0.04
LinearReg 40 - Highest 46 - 0.04
LinearReg 46 - Highest 46 - 0.06
LinearReg 8 - Highest 46 - 0.02
EMA 4 - HMA 10 - 0.11
KAMA 2 - HMA 10 - 0.10
SMA 10 - HMA 10 - 0.10
SMA 6 - HMA 10 - 0.10
SMA 8 - HMA 10 - 0.10
SMMA 4 - HMA 10 - 0.11
KAMA 2 - HMA 12 - 0.10
SMA 10 - HMA 12 - 0.09
SMA 6 - HMA 12 - 0.11
EMA 14 - HMA 14 - 0.11
EMA 6 - HMA 14 - 0.11
EMA 8 - HMA 14 - 0.11
KAMA 10 - HMA 14 - 0.11
KAMA 4 - HMA 14 - 0.10
SMA 12 - HMA 14 - 0.08
SMA 4 - HMA 14 - 0.11
SMA 14 - HMA 26 - 0.11
SMA 16 - HMA 26 - 0.11
SMA 18 - HMA 26 - 0.11
SMA 24 - HMA 26 - 0.11
SMA 26 - HMA 26 - 0.11
SMMA 14 - HMA 26 - 0.11
JMA 26 - HMA 28 - 0.11
JMA 28 - HMA 28 - 0.11
LinearReg 28 - HMA 28 - 0.11
SMA 14 - HMA 28 - 0.11
SMA 16 - HMA 28 - 0.04
SMA 18 - HMA 28 - 0.11
SMA 24 - HMA 28 - 0.09
SMA 26 - HMA 28 - 0.10
SMA 28 - HMA 28 - 0.10
EMA 2 - HMA 4 - 0.00
KAMA 2 - HMA 6 - 0.11
KAMA 4 - HMA 6 - 0.11
KAMA 6 - HMA 6 - 0.10
SMMA 2 - HMA 6 - 0.11
KAMA 6 - HMA 8 - 0.11
SMMA 2 - HMA 8 - 0.11
HMA 8 - JMA 10 - 0.11
KAMA 10 - JMA 10 - 0.11
LinearReg 4 - JMA 10 - 0.11
LinearReg 8 - JMA 10 - 0.11
SMA 10 - JMA 10 - 0.01
HMA 8 - JMA 12 - 0.11
KAMA 10 - JMA 12 - 0.11
KAMA 12 - JMA 12 - 0.11
LinearReg 10 - JMA 12 - 0.11
LinearReg 4 - JMA 12 - 0.11
LinearReg 8 - JMA 12 - 0.10
Lowest 2 - JMA 12 - 0.10
Lowest 8 - JMA 12 - 0.00
SMA 10 - JMA 12 - 0.06
SMA 12 - JMA 12 - 0.09
EMA 10 - JMA 14 - 0.06
EMA 12 - JMA 14 - 0.03
HMA 12 - JMA 14 - 0.09
KAMA 2 - JMA 14 - 0.08
KAMA 4 - JMA 14 - 0.11
KAMA 6 - JMA 14 - 0.11
LinearReg 14 - JMA 14 - 0.11
LinearReg 2 - JMA 14 - 0.11
Lowest 6 - JMA 14 - 0.02
SMMA 10 - JMA 14 - 0.05
SMMA 2 - JMA 14 - 0.08
SMMA 8 - JMA 14 - 0.10
EMA 14 - JMA 16 - 0.07
EMA 16 - JMA 16 - 0.11
LinearReg 18 - Highest 28 - 0.02
LinearReg 12 - Highest 30 - 0.02
LinearReg 18 - Highest 30 - 0.02
LinearReg 24 - Highest 30 - 0.02
LinearReg 8 - Highest 30 - 0.02
LinearReg 12 - Highest 32 - 0.02
LinearReg 18 - Highest 32 - 0.02
LinearReg 8 - Highest 32 - 0.02
LinearReg 18 - Highest 34 - 0.01
LinearReg 20 - Highest 34 - 0.02
LinearReg 26 - Highest 34 - 0.02
LinearReg 34 - Highest 34 - 0.04
LinearReg 12 - Highest 36 - 0.02
LinearReg 18 - Highest 36 - 0.02
LinearReg 34 - Highest 36 - 0.04
LinearReg 14 - Highest 38 - 0.02
LinearReg 20 - Highest 38 - 0.02
LinearReg 22 - Highest 38 - 0.02
LinearReg 26 - Highest 38 - 0.03
LinearReg 14 - Highest 40 - 0.02
LinearReg 14 - Highest 42 - 0.02
LinearReg 20 - Highest 42 - 0.02
LinearReg 22 - Highest 42 - 0.02
LinearReg 26 - Highest 42 - 0.03
LinearReg 14 - Highest 44 - 0.02
LinearReg 22 - Highest 44 - 0.02
LinearReg 28 - Highest 44 - 0.02
LinearReg 30 - Highest 44 - 0.02
LinearReg 36 - Highest 44 - 0.04
LinearReg 44 - Highest 44 - 0.08
LinearReg 14 - Highest 46 - 0.02
LinearReg 20 - Highest 46 - 0.02
LinearReg 22 - Highest 46 - 0.01
LinearReg 26 - Highest 46 - 0.03
LinearReg 6 - Highest 8 - 0.03
LinearReg 8 - Highest 8 - 0.03
EMA 6 - HMA 10 - 0.11
Highest 10 - HMA 10 - 0.11
Highest 6 - HMA 10 - 0.11
KAMA 8 - HMA 10 - 0.10
SMA 4 - HMA 10 - 0.11
EMA 12 - HMA 12 - 0.11
EMA 6 - HMA 12 - 0.11
Highest 10 - HMA 12 - 0.11
Highest 8 - HMA 12 - 0.11
KAMA 10 - HMA 12 - 0.11
KAMA 12 - HMA 12 - 0.11
LinearReg 18 - Highest 38 - 0.02
LinearReg 30 - Highest 38 - 0.02
LinearReg 32 - Highest 38 - 0.03
LinearReg 36 - Highest 38 - 0.04
LinearReg 38 - Highest 38 - 0.04
LinearReg 12 - Highest 40 - 0.02
LinearReg 18 - Highest 40 - 0.02
LinearReg 24 - Highest 40 - 0.03
LinearReg 30 - Highest 40 - 0.02
LinearReg 32 - Highest 40 - 0.03
LinearReg 36 - Highest 40 - 0.04
LinearReg 38 - Highest 40 - 0.04
LinearReg 8 - Highest 40 - 0.02
LinearReg 12 - Highest 42 - 0.02
LinearReg 18 - Highest 42 - 0.02
LinearReg 30 - Highest 42 - 0.03
LinearReg 32 - Highest 42 - 0.03
LinearReg 36 - Highest 42 - 0.04
LinearReg 8 - Highest 42 - 0.02
LinearReg 18 - Highest 44 - 0.02
LinearReg 20 - Highest 44 - 0.02
LinearReg 26 - Highest 44 - 0.03
LinearReg 32 - Highest 44 - 0.03
LinearReg 38 - Highest 44 - 0.04
LinearReg 12 - Highest 46 - 0.02
LinearReg 18 - Highest 46 - 0.03
LinearReg 30 - Highest 46 - 0.01
LinearReg 32 - Highest 46 - 0.03
LinearReg 36 - Highest 46 - 0.04
LinearReg 44 - Highest 46 - 0.08
LinearReg 6 - Highest 6 - 0.04
Highest 2 - HMA 10 - 0.11
Highest 4 - HMA 10 - 0.11
Highest 8 - HMA 10 - 0.11
JMA 10 - HMA 10 - 0.11
KAMA 10 - HMA 10 - 0.10
SMMA 6 - HMA 10 - 0.11
SMA 12 - HMA 12 - 0.10
SMMA 6 - HMA 12 - 0.10
EMA 12 - HMA 14 - 0.11
Highest 10 - HMA 14 - 0.11
KAMA 2 - HMA 14 - 0.11
KAMA 6 - HMA 14 - 0.11
SMA 14 - HMA 14 - 0.07
SMA 6 - HMA 14 - 0.10
SMMA 4 - HMA 14 - 0.11
EMA 14 - HMA 16 - 0.10
SMMA 14 - HMA 14 - 0.11
SMMA 8 - HMA 14 - 0.11
EMA 6 - HMA 16 - 0.11
EMA 8 - HMA 16 - 0.11
KAMA 16 - HMA 16 - 0.08
KAMA 8 - HMA 16 - 0.11
SMA 14 - HMA 16 - 0.11
EMA 12 - HMA 18 - 0.11
EMA 14 - HMA 18 - 0.02
EMA 16 - HMA 18 - 0.03
EMA 6 - HMA 18 - 0.11
Highest 4 - HMA 18 - 0.11
JMA 14 - HMA 18 - 0.11
KAMA 10 - HMA 18 - 0.11
KAMA 12 - HMA 18 - 0.11
KAMA 14 - HMA 18 - 0.11
KAMA 8 - HMA 18 - 0.11
SMA 14 - HMA 18 - 0.11
SMA 16 - HMA 18 - 0.10
SMMA 18 - HMA 18 - 0.05
SMMA 2 - HMA 2 - 0.06
EMA 14 - HMA 20 - 0.11
EMA 16 - HMA 20 - 0.03
EMA 18 - HMA 20 - 0.03
EMA 6 - HMA 20 - 0.11
EMA 8 - HMA 20 - 0.11
KAMA 16 - HMA 20 - 0.11
KAMA 4 - HMA 20 - 0.09
KAMA 8 - HMA 20 - 0.11
SMA 12 - HMA 20 - 0.11
SMMA 14 - HMA 20 - 0.09
SMMA 8 - HMA 20 - 0.11
EMA 14 - HMA 22 - 0.11
EMA 16 - HMA 22 - 0.11
EMA 18 - HMA 22 - 0.11
EMA 6 - HMA 22 - 0.11
EMA 8 - HMA 22 - 0.10
Highest 4 - HMA 22 - 0.11
Highest 6 - HMA 22 - 0.11
JMA 12 - HMA 22 - 0.11
JMA 14 - HMA 22 - 0.11
JMA 16 - HMA 22 - 0.11
JMA 18 - HMA 22 - 0.11
JMA 22 - HMA 22 - 0.10
KAMA 10 - HMA 22 - 0.11
KAMA 18 - HMA 22 - 0.11
KAMA 20 - HMA 22 - 0.11
KAMA 22 - HMA 22 - 0.11
KAMA 4 - HMA 22 - 0.11
SMA 12 - HMA 22 - 0.09
SMA 22 - HMA 22 - 0.11
HMA 6 - JMA 16 - 0.11
LinearReg 2 - JMA 16 - 0.11
Lowest 6 - JMA 16 - 0.11
SMA 6 - JMA 16 - 0.08
SMA 8 - JMA 16 - 0.08
SMMA 14 - JMA 16 - 0.10
SMMA 6 - JMA 16 - 0.07
EMA 10 - JMA 18 - 0.08
EMA 2 - JMA 18 - 0.11
EMA 6 - JMA 18 - 0.11
EMA 8 - JMA 18 - 0.11
HMA 10 - JMA 18 - 0.09
HMA 16 - JMA 18 - 0.09
HMA 4 - JMA 18 - 0.11
KAMA 10 - JMA 18 - 0.07
KAMA 18 - JMA 18 - 0.11
KAMA 8 - JMA 18 - 0.07
LinearReg 12 - JMA 18 - 0.09
LinearReg 18 - JMA 18 - 0.05
SMA 4 - JMA 18 - 0.11
SMMA 12 - JMA 18 - 0.11
SMMA 16 - JMA 18 - 0.11
SMMA 8 - JMA 18 - 0.10
EMA 2 - JMA 2 - 0.04
SMMA 2 - JMA 2 - 0.06
EMA 10 - JMA 20 - 0.09
EMA 12 - JMA 20 - 0.09
EMA 2 - JMA 20 - 0.11
EMA 6 - JMA 20 - 0.11
HMA 10 - JMA 20 - 0.11
KAMA 16 - JMA 20 - 0.11
LinearReg 14 - JMA 20 - 0.08
SMA 4 - JMA 20 - 0.11
SMMA 12 - JMA 20 - 0.10
EMA 10 - JMA 22 - 0.11
EMA 2 - JMA 22 - 0.11
EMA 20 - JMA 22 - 0.10
EMA 6 - JMA 22 - 0.09
EMA 8 - JMA 22 - 0.11
HMA 10 - JMA 22 - 0.10
HMA 16 - JMA 22 - 0.11
HMA 4 - JMA 22 - 0.11
KAMA 16 - JMA 22 - 0.11
KAMA 8 - JMA 22 - 0.11
LinearReg 12 - JMA 22 - 0.10
LinearReg 18 - JMA 22 - 0.06
SMA 4 - JMA 22 - 0.11
SMMA 12 - JMA 22 - 0.10
EMA 14 - JMA 24 - 0.10
EMA 16 - JMA 24 - 0.11
EMA 4 - JMA 24 - 0.11
EMA 16 - HMA 16 - 0.11
KAMA 4 - HMA 16 - 0.08
KAMA 6 - HMA 16 - 0.11
SMA 16 - HMA 16 - 0.10
SMA 6 - HMA 16 - 0.11
SMA 8 - HMA 16 - 0.11
SMMA 14 - HMA 16 - 0.09
SMMA 8 - HMA 16 - 0.11
EMA 10 - HMA 18 - 0.11
EMA 8 - HMA 18 - 0.11
Highest 6 - HMA 18 - 0.11
JMA 16 - HMA 18 - 0.11
JMA 18 - HMA 18 - 0.11
KAMA 16 - HMA 18 - 0.11
KAMA 18 - HMA 18 - 0.11
KAMA 4 - HMA 18 - 0.11
KAMA 6 - HMA 18 - 0.11
SMA 18 - HMA 18 - 0.09
SMMA 10 - HMA 18 - 0.09
SMMA 12 - HMA 18 - 0.09
SMMA 8 - HMA 18 - 0.02
EMA 2 - HMA 2 - 0.09
Highest 2 - HMA 2 - 0.11
EMA 12 - HMA 20 - 0.11
KAMA 20 - HMA 20 - 0.11
KAMA 6 - HMA 20 - 0.08
SMA 14 - HMA 20 - 0.11
SMA 16 - HMA 20 - 0.10
SMA 18 - HMA 20 - 0.09
SMMA 10 - HMA 20 - 0.09
EMA 12 - HMA 22 - 0.11
KAMA 16 - HMA 22 - 0.11
KAMA 6 - HMA 22 - 0.08
LinearReg 22 - HMA 22 - 0.11
SMA 14 - HMA 22 - 0.09
SMA 16 - HMA 22 - 0.09
SMA 18 - HMA 22 - 0.09
SMMA 18 - HMA 22 - 0.10
EMA 20 - HMA 24 - 0.11
EMA 8 - HMA 24 - 0.11
JMA 16 - HMA 24 - 0.11
JMA 18 - HMA 24 - 0.11
JMA 20 - HMA 24 - 0.11
JMA 22 - HMA 24 - 0.11
JMA 24 - HMA 24 - 0.11
KAMA 10 - HMA 24 - 0.11
KAMA 12 - HMA 24 - 0.11
KAMA 18 - HMA 24 - 0.11
KAMA 8 - HMA 24 - 0.08
SMMA 12 - HMA 24 - 0.11
SMMA 16 - HMA 24 - 0.11
KAMA 8 - HMA 12 - 0.09
EMA 10 - HMA 14 - 0.11
Highest 12 - HMA 14 - 0.11
Highest 14 - HMA 14 - 0.11
Highest 4 - HMA 14 - 0.11
SMA 8 - HMA 14 - 0.11
SMMA 10 - HMA 14 - 0.11
SMMA 12 - HMA 14 - 0.11
SMMA 6 - HMA 14 - 0.11
EMA 10 - HMA 16 - 0.11
EMA 12 - HMA 16 - 0.10
KAMA 14 - HMA 16 - 0.11
SMMA 10 - HMA 16 - 0.11
SMMA 6 - HMA 16 - 0.11
SMA 10 - HMA 18 - 0.11
SMA 6 - HMA 18 - 0.11
SMA 8 - HMA 18 - 0.11
SMMA 16 - HMA 18 - 0.09
SMMA 4 - HMA 18 - 0.11
JMA 2 - HMA 2 - 0.11
EMA 10 - HMA 20 - 0.11
JMA 20 - HMA 20 - 0.11
KAMA 12 - HMA 20 - 0.11
KAMA 14 - HMA 20 - 0.11
SMA 6 - HMA 20 - 0.11
SMA 8 - HMA 20 - 0.11
SMMA 12 - HMA 20 - 0.08
SMMA 18 - HMA 20 - 0.10
SMMA 4 - HMA 20 - 0.11
SMMA 6 - HMA 20 - 0.11
EMA 10 - HMA 22 - 0.11
EMA 20 - HMA 22 - 0.11
EMA 22 - HMA 22 - 0.08
SMA 6 - HMA 22 - 0.11
SMA 8 - HMA 22 - 0.11
SMMA 10 - HMA 22 - 0.11
SMMA 12 - HMA 22 - 0.09
SMMA 22 - HMA 22 - 0.10
SMMA 4 - HMA 22 - 0.11
SMMA 6 - HMA 22 - 0.11
EMA 18 - HMA 24 - 0.11
KAMA 16 - HMA 24 - 0.11
KAMA 4 - HMA 24 - 0.10
KAMA 6 - HMA 24 - 0.08
SMA 10 - HMA 24 - 0.11
SMA 12 - HMA 24 - 0.09
SMA 20 - HMA 24 - 0.11
SMMA 8 - HMA 24 - 0.11
EMA 20 - HMA 26 - 0.11
KAMA 26 - HMA 26 - 0.11
KAMA 6 - HMA 26 - 0.09
SMMA 14 - HMA 22 - 0.09
SMMA 8 - HMA 22 - 0.11
EMA 10 - HMA 24 - 0.11
EMA 12 - HMA 24 - 0.11
EMA 22 - HMA 24 - 0.11
EMA 24 - HMA 24 - 0.11
JMA 12 - HMA 24 - 0.11
JMA 14 - HMA 24 - 0.11
KAMA 14 - HMA 24 - 0.11
LinearReg 24 - HMA 24 - 0.11
SMA 16 - HMA 24 - 0.09
SMA 18 - HMA 24 - 0.11
SMA 24 - HMA 24 - 0.11
SMMA 10 - HMA 24 - 0.11
EMA 12 - HMA 26 - 0.11
EMA 14 - HMA 26 - 0.11
EMA 16 - HMA 26 - 0.11
EMA 24 - HMA 26 - 0.11
EMA 26 - HMA 26 - 0.11
JMA 14 - HMA 26 - 0.11
JMA 20 - HMA 26 - 0.11
KAMA 10 - HMA 26 - 0.08
KAMA 12 - HMA 26 - 0.11
KAMA 14 - HMA 26 - 0.11
KAMA 24 - HMA 26 - 0.11
SMMA 6 - HMA 26 - 0.11
EMA 20 - HMA 28 - 0.11
EMA 22 - HMA 28 - 0.11
SMMA 10 - HMA 28 - 0.11
SMA 20 - HMA 30 - 0.11
SMA 20 - HMA 32 - 0.11
SMMA 2 - HMA 4 - 0.03
EMA 2 - HMA 6 - 0.11
EMA 6 - HMA 6 - 0.03
JMA 6 - HMA 6 - 0.11
SMA 4 - HMA 6 - 0.03
EMA 4 - HMA 8 - 0.11
KAMA 2 - HMA 8 - 0.11
SMA 2 - HMA 8 - 0.11
SMA 6 - HMA 8 - 0.03
SMMA 6 - HMA 8 - 0.10
EMA 2 - JMA 10 - 0.10
EMA 6 - JMA 10 - 0.02
EMA 8 - JMA 10 - 0.06
HMA 10 - JMA 10 - 0.11
HMA 6 - JMA 10 - 0.11
KAMA 8 - JMA 10 - 0.11
Lowest 2 - JMA 10 - 0.10
Lowest 4 - JMA 10 - 0.02
Lowest 8 - JMA 10 - 0.02
SMA 4 - JMA 10 - 0.04
HMA 6 - JMA 24 - 0.11
KAMA 12 - JMA 24 - 0.11
KAMA 14 - JMA 24 - 0.11
LinearReg 6 - JMA 24 - 0.11
Lowest 2 - JMA 24 - 0.11
Lowest 8 - JMA 24 - 0.11
SMA 14 - JMA 24 - 0.06
SMA 2 - JMA 24 - 0.11
SMA 22 - JMA 24 - 0.09
SMA 6 - JMA 24 - 0.11
SMA 8 - JMA 24 - 0.11
SMMA 14 - JMA 24 - 0.11
SMMA 22 - JMA 24 - 0.11
SMMA 4 - JMA 24 - 0.11
SMMA 6 - JMA 24 - 0.09
EMA 2 - JMA 26 - 0.11
HMA 14 - JMA 26 - 0.07
HMA 8 - JMA 26 - 0.11
KAMA 12 - JMA 26 - 0.11
KAMA 14 - JMA 26 - 0.11
KAMA 26 - JMA 26 - 0.11
KAMA 8 - JMA 26 - 0.11
LinearReg 10 - JMA 26 - 0.09
LinearReg 12 - JMA 26 - 0.10
LinearReg 16 - JMA 26 - 0.05
LinearReg 24 - JMA 26 - 0.05
LinearReg 4 - JMA 26 - 0.11
LinearReg 6 - JMA 26 - 0.11
LinearReg 8 - JMA 26 - 0.10
Lowest 4 - JMA 26 - 0.08
SMA 10 - JMA 26 - 0.10
SMA 20 - JMA 26 - 0.08
SMMA 16 - JMA 26 - 0.11
SMMA 20 - JMA 26 - 0.11
EMA 12 - JMA 28 - 0.09
EMA 24 - JMA 28 - 0.11
EMA 26 - JMA 28 - 0.11
HMA 12 - JMA 28 - 0.11
KAMA 16 - JMA 28 - 0.11
KAMA 2 - JMA 28 - 0.11
KAMA 6 - JMA 28 - 0.11
LinearReg 14 - JMA 28 - 0.06
LinearReg 2 - JMA 28 - 0.11
LinearReg 22 - JMA 28 - 0.06
LinearReg 28 - JMA 28 - 0.11
SMA 14 - JMA 28 - 0.09
SMA 16 - JMA 28 - 0.09
SMA 18 - JMA 28 - 0.09
SMA 2 - JMA 28 - 0.11
SMA 24 - JMA 28 - 0.09
SMA 12 - HMA 26 - 0.02
SMA 22 - HMA 26 - 0.11
SMMA 8 - HMA 26 - 0.11
EMA 14 - HMA 28 - 0.11
EMA 16 - HMA 28 - 0.11
EMA 18 - HMA 28 - 0.11
JMA 18 - HMA 28 - 0.11
JMA 22 - HMA 28 - 0.11
JMA 24 - HMA 28 - 0.11
SMA 12 - HMA 28 - 0.10
SMA 22 - HMA 28 - 0.11
JMA 24 - HMA 30 - 0.11
JMA 26 - HMA 30 - 0.11
JMA 28 - HMA 30 - 0.11
JMA 30 - HMA 30 - 0.11
SMA 16 - HMA 30 - 0.11
SMA 18 - HMA 30 - 0.09
SMA 24 - HMA 30 - 0.10
SMA 26 - HMA 30 - 0.10
JMA 30 - HMA 32 - 0.11
JMA 32 - HMA 32 - 0.11
Highest 2 - HMA 4 - 0.11
EMA 4 - HMA 6 - 0.00
HMA 4 - HMA 6 - 0.11
JMA 4 - HMA 6 - 0.11
SMA 2 - HMA 6 - 0.00
SMA 6 - HMA 6 - 0.03
SMMA 4 - HMA 6 - 0.03
SMMA 6 - HMA 6 - 0.03
EMA 6 - HMA 8 - 0.11
Highest 6 - HMA 8 - 0.11
JMA 6 - HMA 8 - 0.11
EMA 10 - JMA 10 - 0.08
EMA 4 - JMA 10 - 0.02
HMA 4 - JMA 10 - 0.11
LinearReg 10 - JMA 10 - 0.09
LinearReg 2 - JMA 10 - 0.11
LinearReg 6 - JMA 10 - 0.11
SMA 2 - JMA 10 - 0.10
SMA 6 - JMA 10 - 0.00
SMA 8 - JMA 10 - 0.09
SMMA 4 - JMA 10 - 0.04
SMMA 6 - JMA 10 - 0.08
LinearReg 6 - JMA 12 - 0.11
SMA 2 - JMA 12 - 0.11
SMA 6 - JMA 12 - 0.03
SMA 8 - JMA 12 - 0.07
SMMA 12 - JMA 12 - 0.07
SMMA 6 - JMA 12 - 0.06
EMA 14 - JMA 14 - 0.11
EMA 2 - JMA 14 - 0.08
EMA 2 - JMA 12 - 0.10
EMA 6 - JMA 12 - 0.02
HMA 10 - JMA 12 - 0.11
HMA 6 - JMA 12 - 0.11
KAMA 8 - JMA 12 - 0.10
Lowest 6 - JMA 12 - 0.00
SMA 4 - JMA 12 - 0.05
EMA 4 - JMA 14 - 0.08
HMA 4 - JMA 14 - 0.11
KAMA 14 - JMA 14 - 0.11
LinearReg 6 - JMA 14 - 0.10
SMA 2 - JMA 14 - 0.11
SMA 6 - JMA 14 - 0.06
SMA 8 - JMA 14 - 0.06
SMMA 12 - JMA 14 - 0.07
SMMA 4 - JMA 14 - 0.06
SMMA 6 - JMA 14 - 0.06
EMA 12 - JMA 16 - 0.06
HMA 14 - JMA 16 - 0.11
KAMA 12 - JMA 16 - 0.10
KAMA 14 - JMA 16 - 0.11
LinearReg 10 - JMA 16 - 0.10
LinearReg 4 - JMA 16 - 0.11
LinearReg 6 - JMA 16 - 0.11
LinearReg 8 - JMA 16 - 0.11
SMA 14 - JMA 16 - 0.07
SMA 16 - JMA 16 - 0.06
SMMA 10 - JMA 16 - 0.08
HMA 12 - JMA 18 - 0.11
HMA 8 - JMA 18 - 0.11
KAMA 16 - JMA 18 - 0.10
KAMA 2 - JMA 18 - 0.10
KAMA 4 - JMA 18 - 0.10
KAMA 6 - JMA 18 - 0.08
LinearReg 14 - JMA 18 - 0.11
LinearReg 2 - JMA 18 - 0.11
Lowest 2 - JMA 18 - 0.11
Lowest 4 - JMA 18 - 0.08
SMA 10 - JMA 18 - 0.06
SMMA 2 - JMA 18 - 0.10
EMA 20 - JMA 20 - 0.07
HMA 12 - JMA 20 - 0.11
KAMA 2 - JMA 20 - 0.11
KAMA 4 - JMA 20 - 0.11
KAMA 6 - JMA 20 - 0.11
LinearReg 2 - JMA 20 - 0.11
SMA 10 - JMA 20 - 0.06
SMA 12 - JMA 20 - 0.08
SMA 20 - JMA 20 - 0.06
SMMA 16 - JMA 20 - 0.11
SMMA 2 - JMA 20 - 0.11
SMA 26 - JMA 28 - 0.11
SMA 28 - JMA 28 - 0.11
SMMA 18 - JMA 28 - 0.11
EMA 12 - JMA 30 - 0.11
EMA 2 - JMA 30 - 0.11
EMA 22 - JMA 30 - 0.11
EMA 24 - JMA 30 - 0.11
EMA 26 - JMA 30 - 0.11
EMA 30 - JMA 30 - 0.11
EMA 6 - JMA 30 - 0.11
EMA 8 - JMA 30 - 0.11
HMA 10 - JMA 30 - 0.11
KAMA 16 - JMA 30 - 0.09
KAMA 8 - JMA 30 - 0.09
LinearReg 14 - JMA 30 - 0.11
LinearReg 26 - JMA 30 - 0.11
SMA 4 - JMA 30 - 0.11
SMMA 12 - JMA 30 - 0.11
SMMA 24 - JMA 30 - 0.11
EMA 10 - JMA 32 - 0.11
EMA 2 - JMA 32 - 0.11
EMA 20 - JMA 32 - 0.10
EMA 22 - JMA 32 - 0.11
EMA 30 - JMA 32 - 0.11
EMA 6 - JMA 32 - 0.11
EMA 8 - JMA 32 - 0.11
HMA 10 - JMA 32 - 0.11
KAMA 16 - JMA 32 - 0.11
KAMA 8 - JMA 32 - 0.11
LinearReg 12 - JMA 32 - 0.10
LinearReg 18 - JMA 32 - 0.11
LinearReg 26 - JMA 32 - 0.11
LinearReg 30 - JMA 32 - 0.11
LinearReg 32 - JMA 32 - 0.11
SMA 4 - JMA 32 - 0.11
SMMA 12 - JMA 32 - 0.10
SMMA 8 - JMA 32 - 0.10
EMA 14 - JMA 34 - 0.11
EMA 16 - JMA 34 - 0.11
EMA 4 - JMA 34 - 0.11
HMA 6 - JMA 34 - 0.11
KAMA 12 - JMA 34 - 0.11
KAMA 14 - JMA 34 - 0.11
LinearReg 6 - JMA 34 - 0.11
LinearReg 8 - JMA 34 - 0.11
Lowest 2 - JMA 34 - 0.11
Lowest 4 - JMA 34 - 0.05
SMA 2 - JMA 34 - 0.11
SMA 22 - JMA 34 - 0.11
SMA 32 - JMA 34 - 0.10
SMA 6 - JMA 34 - 0.09
EMA 6 - JMA 14 - 0.05
EMA 8 - JMA 14 - 0.06
HMA 10 - JMA 14 - 0.11
HMA 6 - JMA 14 - 0.11
KAMA 8 - JMA 14 - 0.11
LinearReg 12 - JMA 14 - 0.10
Lowest 2 - JMA 14 - 0.11
Lowest 4 - JMA 14 - 0.05
Lowest 8 - JMA 14 - 0.00
SMA 14 - JMA 14 - 0.08
SMA 4 - JMA 14 - 0.11
SMMA 14 - JMA 14 - 0.07
HMA 12 - JMA 16 - 0.11
KAMA 16 - JMA 16 - 0.11
KAMA 2 - JMA 16 - 0.10
KAMA 6 - JMA 16 - 0.11
LinearReg 14 - JMA 16 - 0.11
Lowest 2 - JMA 16 - 0.11
SMA 10 - JMA 16 - 0.07
SMA 2 - JMA 16 - 0.11
SMMA 2 - JMA 16 - 0.10
EMA 12 - JMA 18 - 0.10
HMA 14 - JMA 18 - 0.11
KAMA 12 - JMA 18 - 0.08
KAMA 14 - JMA 18 - 0.10
LinearReg 10 - JMA 18 - 0.11
LinearReg 16 - JMA 18 - 0.07
LinearReg 4 - JMA 18 - 0.11
SMA 14 - JMA 18 - 0.06
SMA 16 - JMA 18 - 0.07
SMA 18 - JMA 18 - 0.07
SMMA 10 - JMA 18 - 0.11
SMMA 18 - JMA 18 - 0.11
KAMA 2 - JMA 2 - 0.11
LinearReg 2 - JMA 2 - 0.11
SMA 2 - JMA 2 - 0.11
EMA 14 - JMA 20 - 0.10
EMA 16 - JMA 20 - 0.07
HMA 14 - JMA 20 - 0.10
KAMA 10 - JMA 20 - 0.11
KAMA 12 - JMA 20 - 0.11
KAMA 18 - JMA 20 - 0.11
LinearReg 18 - JMA 20 - 0.09
LinearReg 4 - JMA 20 - 0.11
LinearReg 8 - JMA 20 - 0.11
Lowest 6 - JMA 20 - 0.11
SMA 16 - JMA 20 - 0.08
SMA 18 - JMA 20 - 0.06
SMMA 10 - JMA 20 - 0.10
SMMA 18 - JMA 20 - 0.11
SMMA 20 - JMA 20 - 0.11
SMMA 8 - JMA 20 - 0.10
EMA 18 - JMA 22 - 0.10
HMA 12 - JMA 22 - 0.10
HMA 8 - JMA 22 - 0.11
KAMA 2 - JMA 22 - 0.11
KAMA 4 - JMA 22 - 0.11
KAMA 6 - JMA 22 - 0.11
LinearReg 14 - JMA 22 - 0.08
LinearReg 2 - JMA 22 - 0.11
LinearReg 20 - JMA 22 - 0.05
LinearReg 22 - JMA 22 - 0.03
Lowest 2 - JMA 22 - 0.11
Lowest 4 - JMA 22 - 0.10
Lowest 8 - JMA 22 - 0.10
SMA 10 - JMA 22 - 0.10
SMA 12 - JMA 22 - 0.11
SMA 20 - JMA 22 - 0.06
SMMA 16 - JMA 22 - 0.11
SMMA 2 - JMA 22 - 0.11
SMMA 8 - JMA 22 - 0.10
EMA 12 - JMA 24 - 0.08
HMA 14 - JMA 24 - 0.11
KAMA 10 - JMA 24 - 0.11
KAMA 18 - JMA 24 - 0.11
KAMA 20 - JMA 24 - 0.11
KAMA 22 - JMA 24 - 0.11
KAMA 24 - JMA 24 - 0.11
LinearReg 10 - JMA 24 - 0.11
LinearReg 16 - JMA 24 - 0.05
LinearReg 4 - JMA 24 - 0.11
LinearReg 8 - JMA 24 - 0.09
Lowest 6 - JMA 24 - 0.11
SMA 16 - JMA 24 - 0.08
SMA 18 - JMA 24 - 0.09
SMA 24 - JMA 24 - 0.08
SMMA 10 - JMA 24 - 0.11
SMMA 18 - JMA 24 - 0.11
SMMA 20 - JMA 24 - 0.11
EMA 14 - JMA 26 - 0.09
EMA 16 - JMA 26 - 0.09
EMA 18 - JMA 26 - 0.09
EMA 6 - JMA 26 - 0.10
EMA 8 - JMA 26 - 0.09
HMA 10 - JMA 26 - 0.09
HMA 6 - JMA 26 - 0.11
KAMA 10 - JMA 26 - 0.11
KAMA 16 - JMA 26 - 0.11
KAMA 18 - JMA 26 - 0.11
KAMA 20 - JMA 26 - 0.11
KAMA 22 - JMA 26 - 0.11
KAMA 24 - JMA 26 - 0.11
SMA 8 - JMA 34 - 0.11
SMMA 14 - JMA 34 - 0.11
SMMA 22 - JMA 34 - 0.11
SMMA 4 - JMA 34 - 0.11
SMMA 6 - JMA 34 - 0.11
EMA 2 - JMA 36 - 0.11
HMA 14 - JMA 36 - 0.11
KAMA 12 - JMA 36 - 0.11
KAMA 14 - JMA 36 - 0.11
KAMA 8 - JMA 36 - 0.11
LinearReg 10 - JMA 36 - 0.11
LinearReg 12 - JMA 36 - 0.09
LinearReg 16 - JMA 36 - 0.10
LinearReg 24 - JMA 36 - 0.11
LinearReg 30 - JMA 36 - 0.11
LinearReg 36 - JMA 36 - 0.11
LinearReg 4 - JMA 36 - 0.11
LinearReg 6 - JMA 36 - 0.11
LinearReg 8 - JMA 36 - 0.11
SMA 10 - JMA 36 - 0.11
SMA 20 - JMA 36 - 0.11
SMA 30 - JMA 36 - 0.10
SMA 32 - JMA 36 - 0.11
SMMA 16 - JMA 36 - 0.11
SMMA 20 - JMA 36 - 0.11
EMA 12 - JMA 38 - 0.11
EMA 24 - JMA 38 - 0.11
EMA 26 - JMA 38 - 0.11
KAMA 2 - JMA 38 - 0.11
KAMA 6 - JMA 38 - 0.10
LinearReg 14 - JMA 38 - 0.10
LinearReg 2 - JMA 38 - 0.11
LinearReg 22 - JMA 38 - 0.11
LinearReg 28 - JMA 38 - 0.11
SMA 14 - JMA 38 - 0.10
SMA 16 - JMA 38 - 0.11
SMA 18 - JMA 38 - 0.11
SMA 2 - JMA 38 - 0.11
SMA 24 - JMA 38 - 0.10
SMA 26 - JMA 38 - 0.09
SMA 28 - JMA 38 - 0.10
SMA 34 - JMA 38 - 0.11
SMA 6 - JMA 38 - 0.11
SMMA 18 - JMA 38 - 0.11
SMMA 4 - JMA 38 - 0.09
SMA 2 - JMA 4 - 0.06
EMA 14 - JMA 40 - 0.11
EMA 16 - JMA 40 - 0.11
EMA 24 - JMA 40 - 0.11
EMA 28 - JMA 40 - 0.11
EMA 12 - JMA 22 - 0.07
EMA 22 - JMA 22 - 0.11
HMA 14 - JMA 22 - 0.11
KAMA 10 - JMA 22 - 0.11
KAMA 18 - JMA 22 - 0.11
KAMA 20 - JMA 22 - 0.11
KAMA 22 - JMA 22 - 0.11
LinearReg 10 - JMA 22 - 0.10
LinearReg 16 - JMA 22 - 0.11
LinearReg 4 - JMA 22 - 0.11
LinearReg 8 - JMA 22 - 0.10
SMA 16 - JMA 22 - 0.06
SMA 18 - JMA 22 - 0.07
SMMA 10 - JMA 22 - 0.10
SMMA 18 - JMA 22 - 0.11
SMMA 20 - JMA 22 - 0.11
EMA 18 - JMA 24 - 0.10
EMA 20 - JMA 24 - 0.08
HMA 12 - JMA 24 - 0.08
HMA 8 - JMA 24 - 0.10
KAMA 2 - JMA 24 - 0.11
KAMA 4 - JMA 24 - 0.11
KAMA 6 - JMA 24 - 0.11
LinearReg 14 - JMA 24 - 0.09
LinearReg 2 - JMA 24 - 0.11
LinearReg 22 - JMA 24 - 0.06
SMA 10 - JMA 24 - 0.10
SMA 12 - JMA 24 - 0.06
SMA 20 - JMA 24 - 0.10
SMMA 16 - JMA 24 - 0.11
SMMA 2 - JMA 24 - 0.11
SMMA 8 - JMA 24 - 0.10
EMA 10 - JMA 26 - 0.09
EMA 20 - JMA 26 - 0.11
EMA 22 - JMA 26 - 0.11
EMA 4 - JMA 26 - 0.11
LinearReg 2 - JMA 26 - 0.11
SMA 8 - JMA 26 - 0.11
SMMA 10 - JMA 26 - 0.10
SMMA 12 - JMA 26 - 0.11
SMMA 22 - JMA 26 - 0.11
SMMA 6 - JMA 26 - 0.09
EMA 14 - JMA 28 - 0.08
EMA 16 - JMA 28 - 0.11
EMA 18 - JMA 28 - 0.10
EMA 2 - JMA 28 - 0.11
EMA 28 - JMA 28 - 0.10
EMA 6 - JMA 28 - 0.10
EMA 8 - JMA 28 - 0.10
HMA 10 - JMA 28 - 0.11
HMA 6 - JMA 28 - 0.11
KAMA 4 - JMA 26 - 0.11
LinearReg 18 - JMA 26 - 0.07
LinearReg 20 - JMA 26 - 0.05
LinearReg 26 - JMA 26 - 0.03
Lowest 2 - JMA 26 - 0.09
Lowest 6 - JMA 26 - 0.11
Lowest 8 - JMA 26 - 0.10
SMA 12 - JMA 26 - 0.08
SMA 22 - JMA 26 - 0.11
SMA 4 - JMA 26 - 0.11
SMMA 14 - JMA 26 - 0.11
SMMA 2 - JMA 26 - 0.11
SMMA 24 - JMA 26 - 0.11
SMMA 8 - JMA 26 - 0.11
EMA 10 - JMA 28 - 0.07
EMA 20 - JMA 28 - 0.09
EMA 22 - JMA 28 - 0.11
EMA 4 - JMA 28 - 0.11
LinearReg 4 - JMA 28 - 0.11
LinearReg 6 - JMA 28 - 0.11
LinearReg 8 - JMA 28 - 0.10
SMA 6 - JMA 28 - 0.11
SMMA 10 - JMA 28 - 0.09
SMMA 12 - JMA 28 - 0.11
SMMA 22 - JMA 28 - 0.11
SMMA 4 - JMA 28 - 0.10
SMMA 6 - JMA 28 - 0.09
EMA 10 - JMA 30 - 0.11
EMA 20 - JMA 30 - 0.11
HMA 12 - JMA 30 - 0.11
HMA 8 - JMA 30 - 0.11
KAMA 2 - JMA 30 - 0.11
KAMA 4 - JMA 30 - 0.11
KAMA 6 - JMA 30 - 0.11
LinearReg 2 - JMA 30 - 0.11
LinearReg 20 - JMA 30 - 0.11
LinearReg 22 - JMA 30 - 0.11
LinearReg 28 - JMA 30 - 0.11
LinearReg 30 - JMA 30 - 0.11
SMA 10 - JMA 30 - 0.11
SMA 12 - JMA 30 - 0.11
SMA 20 - JMA 30 - 0.11
SMA 30 - JMA 30 - 0.11
SMMA 16 - JMA 30 - 0.11
SMMA 2 - JMA 30 - 0.11
SMMA 8 - JMA 30 - 0.11
EMA 18 - JMA 32 - 0.09
HMA 12 - JMA 32 - 0.11
HMA 8 - JMA 32 - 0.11
KAMA 2 - JMA 32 - 0.11
HMA 12 - JMA 40 - 0.09
KAMA 2 - JMA 40 - 0.11
KAMA 4 - JMA 40 - 0.11
KAMA 6 - JMA 40 - 0.11
LinearReg 20 - JMA 40 - 0.09
LinearReg 22 - JMA 40 - 0.11
LinearReg 26 - JMA 40 - 0.11
LinearReg 34 - JMA 40 - 0.11
Lowest 6 - JMA 40 - 0.11
SMA 16 - JMA 40 - 0.11
SMA 18 - JMA 40 - 0.11
SMA 26 - JMA 40 - 0.10
SMA 28 - JMA 40 - 0.10
SMA 34 - JMA 40 - 0.11
SMA 36 - JMA 40 - 0.11
SMMA 10 - JMA 40 - 0.11
SMMA 18 - JMA 40 - 0.11
SMMA 2 - JMA 40 - 0.11
SMMA 8 - JMA 40 - 0.11
EMA 10 - JMA 42 - 0.11
EMA 12 - JMA 42 - 0.11
EMA 22 - JMA 42 - 0.11
EMA 24 - JMA 42 - 0.11
EMA 26 - JMA 42 - 0.11
HMA 12 - JMA 42 - 0.08
KAMA 2 - JMA 42 - 0.11
KAMA 4 - JMA 42 - 0.11
KAMA 6 - JMA 42 - 0.11
LinearReg 14 - JMA 42 - 0.11
LinearReg 20 - JMA 42 - 0.10
LinearReg 22 - JMA 42 - 0.11
LinearReg 26 - JMA 42 - 0.11
LinearReg 38 - JMA 42 - 0.11
LinearReg 40 - JMA 42 - 0.11
SMA 16 - JMA 42 - 0.11
SMA 18 - JMA 42 - 0.10
SMA 26 - JMA 42 - 0.09
SMA 28 - JMA 42 - 0.10
SMA 34 - JMA 42 - 0.11
SMA 36 - JMA 42 - 0.11
SMMA 10 - JMA 42 - 0.11
SMMA 18 - JMA 42 - 0.11
SMMA 2 - JMA 42 - 0.11
SMMA 8 - JMA 42 - 0.11
EMA 18 - JMA 44 - 0.11
EMA 20 - JMA 44 - 0.11
HMA 14 - JMA 44 - 0.03
KAMA 10 - JMA 44 - 0.11
KAMA 12 - JMA 44 - 0.10
LinearReg 10 - JMA 44 - 0.11
KAMA 10 - JMA 28 - 0.11
KAMA 18 - JMA 28 - 0.11
KAMA 20 - JMA 28 - 0.11
KAMA 22 - JMA 28 - 0.11
KAMA 24 - JMA 28 - 0.11
KAMA 4 - JMA 28 - 0.11
LinearReg 18 - JMA 28 - 0.05
LinearReg 26 - JMA 28 - 0.11
Lowest 2 - JMA 28 - 0.10
Lowest 6 - JMA 28 - 0.11
Lowest 8 - JMA 28 - 0.11
SMA 12 - JMA 28 - 0.09
SMA 22 - JMA 28 - 0.08
SMA 4 - JMA 28 - 0.10
SMMA 14 - JMA 28 - 0.11
SMMA 2 - JMA 28 - 0.11
SMMA 24 - JMA 28 - 0.11
SMMA 8 - JMA 28 - 0.08
EMA 14 - JMA 30 - 0.11
EMA 16 - JMA 30 - 0.11
EMA 28 - JMA 30 - 0.11
HMA 14 - JMA 30 - 0.11
KAMA 10 - JMA 30 - 0.11
KAMA 18 - JMA 30 - 0.10
KAMA 20 - JMA 30 - 0.11
KAMA 22 - JMA 30 - 0.11
LinearReg 12 - JMA 30 - 0.09
LinearReg 18 - JMA 30 - 0.11
LinearReg 24 - JMA 30 - 0.11
LinearReg 4 - JMA 30 - 0.11
LinearReg 8 - JMA 30 - 0.11
Lowest 6 - JMA 30 - 0.06
SMA 16 - JMA 30 - 0.11
SMA 18 - JMA 30 - 0.11
SMA 24 - JMA 30 - 0.11
SMA 26 - JMA 30 - 0.11
SMA 28 - JMA 30 - 0.11
SMMA 10 - JMA 30 - 0.11
SMMA 18 - JMA 30 - 0.11
SMMA 20 - JMA 30 - 0.11
EMA 12 - JMA 32 - 0.11
EMA 24 - JMA 32 - 0.11
EMA 26 - JMA 32 - 0.11
HMA 14 - JMA 32 - 0.11
KAMA 10 - JMA 32 - 0.10
KAMA 18 - JMA 32 - 0.11
KAMA 20 - JMA 32 - 0.11
KAMA 22 - JMA 32 - 0.11
LinearReg 10 - JMA 32 - 0.11
LinearReg 16 - JMA 32 - 0.11
KAMA 6 - JMA 32 - 0.11
LinearReg 14 - JMA 32 - 0.10
LinearReg 2 - JMA 32 - 0.11
LinearReg 20 - JMA 32 - 0.11
LinearReg 22 - JMA 32 - 0.11
LinearReg 28 - JMA 32 - 0.11
Lowest 2 - JMA 32 - 0.11
SMA 10 - JMA 32 - 0.11
SMA 20 - JMA 32 - 0.08
SMA 30 - JMA 32 - 0.11
SMMA 16 - JMA 32 - 0.11
SMMA 2 - JMA 32 - 0.11
EMA 12 - JMA 34 - 0.11
EMA 28 - JMA 34 - 0.11
HMA 14 - JMA 34 - 0.09
KAMA 10 - JMA 34 - 0.11
KAMA 18 - JMA 34 - 0.11
KAMA 20 - JMA 34 - 0.11
KAMA 22 - JMA 34 - 0.11
LinearReg 10 - JMA 34 - 0.06
LinearReg 16 - JMA 34 - 0.09
LinearReg 24 - JMA 34 - 0.11
LinearReg 34 - JMA 34 - 0.11
LinearReg 4 - JMA 34 - 0.11
Lowest 6 - JMA 34 - 0.11
Lowest 8 - JMA 34 - 0.10
SMA 14 - JMA 34 - 0.11
SMA 16 - JMA 34 - 0.09
SMA 18 - JMA 34 - 0.10
SMA 24 - JMA 34 - 0.11
SMA 26 - JMA 34 - 0.11
SMA 28 - JMA 34 - 0.10
SMA 34 - JMA 34 - 0.11
SMMA 10 - JMA 34 - 0.09
SMMA 18 - JMA 34 - 0.11
SMMA 20 - JMA 34 - 0.11
EMA 14 - JMA 36 - 0.11
EMA 16 - JMA 36 - 0.11
EMA 18 - JMA 36 - 0.11
EMA 28 - JMA 36 - 0.11
EMA 6 - JMA 36 - 0.11
EMA 8 - JMA 36 - 0.11
HMA 10 - JMA 36 - 0.11
HMA 12 - JMA 36 - 0.09
HMA 6 - JMA 36 - 0.11
KAMA 10 - JMA 36 - 0.11
KAMA 16 - JMA 36 - 0.11
KAMA 18 - JMA 36 - 0.11
KAMA 20 - JMA 36 - 0.11
KAMA 4 - JMA 36 - 0.11
EMA 22 - HMA 26 - 0.11
JMA 16 - HMA 26 - 0.11
JMA 18 - HMA 26 - 0.11
JMA 22 - HMA 26 - 0.11
JMA 24 - HMA 26 - 0.10
JMA 26 - HMA 26 - 0.11
KAMA 16 - HMA 26 - 0.11
KAMA 18 - HMA 26 - 0.11
KAMA 8 - HMA 26 - 0.09
LinearReg 26 - HMA 26 - 0.11
SMA 10 - HMA 26 - 0.11
SMA 20 - HMA 26 - 0.11
SMMA 10 - HMA 26 - 0.11
SMMA 12 - HMA 26 - 0.11
JMA 20 - HMA 28 - 0.10
KAMA 28 - HMA 28 - 0.11
SMA 20 - HMA 28 - 0.11
SMA 22 - HMA 30 - 0.11
SMA 22 - HMA 32 - 0.11
KAMA 2 - HMA 4 - 0.11
SMA 2 - HMA 4 - 0.01
Highest 2 - HMA 6 - 0.11
Highest 4 - HMA 6 - 0.11
Highest 6 - HMA 6 - 0.11
Highest 2 - HMA 8 - 0.11
Highest 4 - HMA 8 - 0.11
KAMA 2 - JMA 10 - 0.11
KAMA 4 - JMA 10 - 0.11
KAMA 6 - JMA 10 - 0.11
Lowest 6 - JMA 10 - 0.04
SMMA 10 - JMA 10 - 0.07
SMMA 2 - JMA 10 - 0.00
SMMA 8 - JMA 10 - 0.07
EMA 10 - JMA 12 - 0.06
EMA 12 - JMA 12 - 0.11
KAMA 2 - JMA 12 - 0.08
KAMA 6 - JMA 12 - 0.11
LinearReg 2 - JMA 12 - 0.11
SMMA 10 - JMA 12 - 0.05
SMMA 2 - JMA 12 - 0.04
SMMA 8 - JMA 12 - 0.06
HMA 14 - JMA 14 - 0.11
HMA 8 - JMA 14 - 0.11
KAMA 10 - JMA 14 - 0.11
KAMA 12 - JMA 14 - 0.10
LinearReg 10 - JMA 14 - 0.10
LinearReg 4 - JMA 14 - 0.11
LinearReg 8 - JMA 14 - 0.11
SMA 10 - JMA 14 - 0.06
SMA 12 - JMA 14 - 0.07
EMA 10 - JMA 16 - 0.06
LinearReg 16 - JMA 44 - 0.07
LinearReg 24 - JMA 44 - 0.11
LinearReg 30 - JMA 44 - 0.11
LinearReg 32 - JMA 44 - 0.11
LinearReg 36 - JMA 44 - 0.11
LinearReg 4 - JMA 44 - 0.11
LinearReg 44 - JMA 44 - 0.11
LinearReg 8 - JMA 44 - 0.09
SMA 10 - JMA 44 - 0.11
SMA 12 - JMA 44 - 0.10
SMA 20 - JMA 44 - 0.11
SMA 22 - JMA 44 - 0.11
SMA 30 - JMA 44 - 0.11
SMMA 16 - JMA 44 - 0.11
SMMA 20 - JMA 44 - 0.10
SMMA 22 - JMA 44 - 0.11
EMA 10 - JMA 46 - 0.11
EMA 2 - JMA 46 - 0.11
EMA 20 - JMA 46 - 0.11
EMA 22 - JMA 46 - 0.11
EMA 30 - JMA 46 - 0.11
EMA 6 - JMA 46 - 0.09
EMA 8 - JMA 46 - 0.11
HMA 10 - JMA 46 - 0.08
KAMA 10 - JMA 46 - 0.11
KAMA 18 - JMA 46 - 0.11
KAMA 8 - JMA 46 - 0.11
LinearReg 12 - JMA 46 - 0.11
LinearReg 18 - JMA 46 - 0.05
LinearReg 26 - JMA 46 - 0.11
LinearReg 30 - JMA 46 - 0.11
LinearReg 32 - JMA 46 - 0.11
LinearReg 38 - JMA 46 - 0.11
LinearReg 4 - JMA 46 - 0.11
SMA 30 - JMA 46 - 0.11
SMA 4 - JMA 46 - 0.07
SMMA 12 - JMA 46 - 0.11
SMMA 16 - JMA 46 - 0.11
SMMA 8 - JMA 46 - 0.11
KAMA 4 - JMA 6 - 0.08
KAMA 6 - JMA 6 - 0.11
SMMA 2 - JMA 6 - 0.02
KAMA 6 - JMA 8 - 0.11
SMMA 2 - JMA 8 - 0.02
SMA 6 - KAMA 6 - 0.11
SMMA 8 - KAMA 8 - 0.11
EMA 4 - LinearReg 10 - 0.11
SMA 6 - LinearReg 10 - 0.09
SMA 8 - LinearReg 10 - 0.11
SMMA 4 - LinearReg 10 - 0.11
LinearReg 24 - JMA 32 - 0.11
LinearReg 4 - JMA 32 - 0.11
SMA 14 - JMA 32 - 0.11
SMA 16 - JMA 32 - 0.11
SMA 18 - JMA 32 - 0.10
SMA 24 - JMA 32 - 0.11
SMA 26 - JMA 32 - 0.11
SMA 28 - JMA 32 - 0.11
SMMA 10 - JMA 32 - 0.09
SMMA 18 - JMA 32 - 0.11
SMMA 20 - JMA 32 - 0.11
EMA 18 - JMA 34 - 0.11
EMA 20 - JMA 34 - 0.11
EMA 22 - JMA 34 - 0.11
HMA 12 - JMA 34 - 0.11
HMA 8 - JMA 34 - 0.11
KAMA 2 - JMA 34 - 0.11
KAMA 4 - JMA 34 - 0.11
KAMA 6 - JMA 34 - 0.11
LinearReg 14 - JMA 34 - 0.11
LinearReg 2 - JMA 34 - 0.11
LinearReg 20 - JMA 34 - 0.10
LinearReg 22 - JMA 34 - 0.11
LinearReg 28 - JMA 34 - 0.11
LinearReg 30 - JMA 34 - 0.11
LinearReg 32 - JMA 34 - 0.11
SMA 10 - JMA 34 - 0.11
SMA 12 - JMA 34 - 0.11
SMA 20 - JMA 34 - 0.11
SMA 30 - JMA 34 - 0.10
SMMA 16 - JMA 34 - 0.11
SMMA 2 - JMA 34 - 0.11
EMA 10 - JMA 36 - 0.11
EMA 20 - JMA 36 - 0.11
EMA 22 - JMA 36 - 0.11
EMA 30 - JMA 36 - 0.11
EMA 4 - JMA 36 - 0.11
LinearReg 2 - JMA 36 - 0.11
LinearReg 32 - JMA 36 - 0.11
SMMA 10 - JMA 36 - 0.11
SMMA 12 - JMA 36 - 0.11
SMMA 22 - JMA 36 - 0.11
SMMA 6 - JMA 36 - 0.11
EMA 14 - JMA 38 - 0.11
EMA 16 - JMA 38 - 0.11
EMA 18 - JMA 38 - 0.11
EMA 28 - JMA 38 - 0.11
EMA 6 - JMA 38 - 0.11
EMA 8 - JMA 38 - 0.11
HMA 10 - JMA 38 - 0.11
LinearReg 18 - JMA 36 - 0.07
LinearReg 20 - JMA 36 - 0.11
LinearReg 26 - JMA 36 - 0.11
LinearReg 34 - JMA 36 - 0.11
Lowest 2 - JMA 36 - 0.11
Lowest 4 - JMA 36 - 0.08
Lowest 6 - JMA 36 - 0.11
SMA 12 - JMA 36 - 0.11
SMA 22 - JMA 36 - 0.11
SMA 34 - JMA 36 - 0.11
SMA 4 - JMA 36 - 0.10
SMMA 14 - JMA 36 - 0.11
SMMA 2 - JMA 36 - 0.11
SMMA 8 - JMA 36 - 0.11
EMA 10 - JMA 38 - 0.11
EMA 20 - JMA 38 - 0.09
EMA 22 - JMA 38 - 0.11
EMA 30 - JMA 38 - 0.11
EMA 4 - JMA 38 - 0.11
LinearReg 32 - JMA 38 - 0.11
LinearReg 38 - JMA 38 - 0.11
LinearReg 4 - JMA 38 - 0.11
LinearReg 6 - JMA 38 - 0.11
LinearReg 8 - JMA 38 - 0.09
SMA 36 - JMA 38 - 0.11
SMA 8 - JMA 38 - 0.11
SMMA 10 - JMA 38 - 0.11
SMMA 12 - JMA 38 - 0.10
SMMA 22 - JMA 38 - 0.11
SMMA 6 - JMA 38 - 0.11
LinearReg 4 - JMA 4 - 0.11
EMA 10 - JMA 40 - 0.11
EMA 12 - JMA 40 - 0.11
EMA 22 - JMA 40 - 0.11
EMA 26 - JMA 40 - 0.11
EMA 30 - JMA 40 - 0.11
EMA 4 - JMA 40 - 0.11
HMA 8 - JMA 40 - 0.11
KAMA 14 - JMA 40 - 0.11
LinearReg 10 - JMA 40 - 0.05
LinearReg 16 - JMA 40 - 0.09
LinearReg 2 - JMA 40 - 0.11
LinearReg 28 - JMA 40 - 0.11
LinearReg 38 - JMA 40 - 0.11
LinearReg 40 - JMA 40 - 0.11
LinearReg 6 - JMA 40 - 0.09
SMA 2 - JMA 40 - 0.11
SMA 6 - JMA 40 - 0.11
SMA 8 - JMA 40 - 0.11
SMMA 12 - JMA 40 - 0.11
EMA 2 - JMA 16 - 0.11
EMA 6 - JMA 16 - 0.06
EMA 8 - JMA 16 - 0.08
HMA 10 - JMA 16 - 0.11
HMA 4 - JMA 16 - 0.11
KAMA 10 - JMA 16 - 0.10
KAMA 8 - JMA 16 - 0.10
SMMA 12 - JMA 16 - 0.08
SMMA 16 - JMA 16 - 0.10
SMMA 8 - JMA 16 - 0.10
EMA 14 - JMA 18 - 0.07
EMA 16 - JMA 18 - 0.08
EMA 18 - JMA 18 - 0.11
EMA 4 - JMA 18 - 0.11
HMA 18 - JMA 18 - 0.09
HMA 6 - JMA 18 - 0.11
LinearReg 6 - JMA 18 - 0.11
LinearReg 8 - JMA 18 - 0.11
Lowest 6 - JMA 18 - 0.01
Lowest 8 - JMA 18 - 0.00
SMA 12 - JMA 18 - 0.06
SMA 2 - JMA 18 - 0.11
SMA 6 - JMA 18 - 0.11
SMA 8 - JMA 18 - 0.10
SMMA 14 - JMA 18 - 0.09
SMMA 4 - JMA 18 - 0.11
SMMA 6 - JMA 18 - 0.07
Lowest 2 - JMA 2 - 0.06
EMA 18 - JMA 20 - 0.07
HMA 18 - JMA 20 - 0.11
HMA 6 - JMA 20 - 0.11
KAMA 14 - JMA 20 - 0.11
KAMA 20 - JMA 20 - 0.11
LinearReg 10 - JMA 20 - 0.11
LinearReg 6 - JMA 20 - 0.11
Lowest 2 - JMA 20 - 0.11
SMA 14 - JMA 20 - 0.05
SMA 2 - JMA 20 - 0.11
SMA 6 - JMA 20 - 0.11
SMMA 14 - JMA 20 - 0.10
SMMA 6 - JMA 20 - 0.11
EMA 14 - JMA 22 - 0.08
EMA 16 - JMA 22 - 0.10
EMA 4 - JMA 22 - 0.11
HMA 6 - JMA 22 - 0.11
KAMA 12 - JMA 22 - 0.11
KAMA 14 - JMA 22 - 0.11
LinearReg 6 - JMA 22 - 0.11
Lowest 6 - JMA 22 - 0.11
SMA 14 - JMA 22 - 0.08
SMA 2 - JMA 22 - 0.11
SMMA 6 - LinearReg 10 - 0.11
Highest 10 - LinearReg 12 - 0.11
JMA 6 - LinearReg 12 - 0.11
KAMA 2 - LinearReg 12 - 0.11
KAMA 6 - LinearReg 12 - 0.11
SMA 6 - LinearReg 12 - 0.02
SMMA 4 - LinearReg 12 - 0.10
EMA 12 - LinearReg 14 - 0.10
EMA 6 - LinearReg 14 - 0.11
EMA 8 - LinearReg 14 - 0.11
Highest 14 - LinearReg 14 - 0.11
HMA 10 - LinearReg 14 - 0.11
KAMA 8 - LinearReg 14 - 0.09
SMA 4 - LinearReg 14 - 0.11
SMMA 12 - LinearReg 14 - 0.09
SMMA 8 - LinearReg 14 - 0.09
Highest 2 - LinearReg 16 - 0.11
Highest 6 - LinearReg 16 - 0.11
Highest 8 - LinearReg 16 - 0.11
HMA 14 - LinearReg 16 - 0.11
JMA 14 - LinearReg 16 - 0.11
JMA 8 - LinearReg 16 - 0.10
KAMA 10 - LinearReg 16 - 0.11
KAMA 12 - LinearReg 16 - 0.11
SMA 10 - LinearReg 16 - 0.11
SMMA 16 - LinearReg 16 - 0.09
EMA 18 - LinearReg 18 - 0.11
Highest 4 - LinearReg 18 - 0.11
Highest 8 - LinearReg 18 - 0.11
HMA 14 - LinearReg 18 - 0.11
HMA 18 - LinearReg 18 - 0.10
JMA 12 - LinearReg 18 - 0.11
JMA 14 - LinearReg 18 - 0.11
KAMA 10 - LinearReg 18 - 0.11
KAMA 12 - LinearReg 18 - 0.11
KAMA 14 - LinearReg 18 - 0.11
SMA 12 - LinearReg 18 - 0.09
SMMA 14 - LinearReg 18 - 0.11
SMMA 16 - LinearReg 18 - 0.11
SMMA 6 - LinearReg 18 - 0.11
JMA 2 - LinearReg 2 - 0.11
SMMA 2 - LinearReg 2 - 0.06
EMA 10 - LinearReg 20 - 0.10
EMA 8 - LinearReg 20 - 0.11
Highest 12 - LinearReg 20 - 0.11
Highest 14 - LinearReg 20 - 0.11
Highest 18 - LinearReg 20 - 0.11
Highest 6 - LinearReg 20 - 0.11
HMA 16 - LinearReg 20 - 0.11
HMA 20 - LinearReg 20 - 0.11
JMA 18 - LinearReg 20 - 0.11
KAMA 16 - LinearReg 20 - 0.11
KAMA 18 - LinearReg 20 - 0.11
KAMA 4 - LinearReg 20 - 0.11
SMMA 10 - LinearReg 20 - 0.11
SMMA 12 - LinearReg 20 - 0.10
SMMA 8 - LinearReg 20 - 0.11
EMA 10 - LinearReg 22 - 0.11
EMA 22 - LinearReg 22 - 0.11
Highest 6 - LinearReg 22 - 0.11
HMA 22 - LinearReg 22 - 0.11
JMA 22 - LinearReg 22 - 0.11
KAMA 16 - LinearReg 22 - 0.11
KAMA 18 - LinearReg 22 - 0.11
KAMA 8 - LinearReg 22 - 0.11
SMA 10 - LinearReg 22 - 0.11
SMA 20 - LinearReg 22 - 0.11
Highest 16 - LinearReg 24 - 0.11
JMA 20 - LinearReg 24 - 0.11
KAMA 12 - LinearReg 24 - 0.11
KAMA 14 - LinearReg 24 - 0.11
SMA 12 - LinearReg 24 - 0.10
SMA 22 - LinearReg 24 - 0.10
SMA 8 - LinearReg 24 - 0.11
SMMA 14 - LinearReg 24 - 0.10
SMMA 16 - LinearReg 24 - 0.11
SMMA 22 - LinearReg 24 - 0.11
SMMA 6 - LinearReg 24 - 0.11
EMA 12 - LinearReg 26 - 0.11
Highest 10 - LinearReg 26 - 0.11
Highest 12 - LinearReg 26 - 0.11
Highest 18 - LinearReg 26 - 0.11
Highest 22 - LinearReg 26 - 0.11
Highest 24 - LinearReg 26 - 0.11
KAMA 20 - LinearReg 26 - 0.11
KAMA 22 - LinearReg 26 - 0.11
KAMA 24 - LinearReg 26 - 0.11
KAMA 6 - LinearReg 26 - 0.11
SMA 14 - LinearReg 26 - 0.11
SMA 16 - LinearReg 26 - 0.11
SMA 18 - LinearReg 26 - 0.11
SMA 24 - LinearReg 26 - 0.11
SMA 26 - LinearReg 26 - 0.11
SMMA 10 - LinearReg 26 - 0.11
HMA 12 - JMA 38 - 0.08
HMA 6 - JMA 38 - 0.11
KAMA 10 - JMA 38 - 0.11
KAMA 16 - JMA 38 - 0.11
KAMA 18 - JMA 38 - 0.11
KAMA 20 - JMA 38 - 0.11
KAMA 4 - JMA 38 - 0.10
LinearReg 18 - JMA 38 - 0.09
LinearReg 20 - JMA 38 - 0.11
LinearReg 26 - JMA 38 - 0.11
LinearReg 34 - JMA 38 - 0.11
Lowest 2 - JMA 38 - 0.10
Lowest 6 - JMA 38 - 0.11
Lowest 8 - JMA 38 - 0.11
SMA 12 - JMA 38 - 0.11
SMA 22 - JMA 38 - 0.11
SMA 30 - JMA 38 - 0.10
SMA 32 - JMA 38 - 0.11
SMA 4 - JMA 38 - 0.10
SMMA 14 - JMA 38 - 0.10
SMMA 2 - JMA 38 - 0.11
SMMA 8 - JMA 38 - 0.11
KAMA 2 - JMA 4 - 0.10
LinearReg 2 - JMA 4 - 0.11
Lowest 2 - JMA 4 - 0.10
Lowest 4 - JMA 4 - 0.04
SMMA 2 - JMA 4 - 0.01
EMA 18 - JMA 40 - 0.11
EMA 2 - JMA 40 - 0.11
EMA 6 - JMA 40 - 0.08
EMA 8 - JMA 40 - 0.11
HMA 10 - JMA 40 - 0.11
KAMA 16 - JMA 40 - 0.11
KAMA 18 - JMA 40 - 0.11
KAMA 8 - JMA 40 - 0.11
LinearReg 14 - JMA 40 - 0.11
Lowest 2 - JMA 40 - 0.09
SMA 14 - JMA 40 - 0.11
SMA 24 - JMA 40 - 0.11
SMA 32 - JMA 40 - 0.11
SMA 4 - JMA 40 - 0.06
SMMA 14 - JMA 40 - 0.11
EMA 14 - JMA 42 - 0.11
EMA 16 - JMA 42 - 0.11
EMA 2 - JMA 42 - 0.11
EMA 28 - JMA 42 - 0.11
EMA 6 - JMA 42 - 0.11
EMA 8 - JMA 42 - 0.11
HMA 10 - JMA 42 - 0.06
HMA 6 - JMA 42 - 0.11
KAMA 16 - JMA 42 - 0.11
SMMA 4 - JMA 40 - 0.11
SMMA 6 - JMA 40 - 0.11
EMA 20 - JMA 42 - 0.11
EMA 30 - JMA 42 - 0.11
EMA 4 - JMA 42 - 0.11
KAMA 14 - JMA 42 - 0.10
LinearReg 2 - JMA 42 - 0.11
LinearReg 28 - JMA 42 - 0.11
LinearReg 30 - JMA 42 - 0.11
LinearReg 32 - JMA 42 - 0.11
LinearReg 36 - JMA 42 - 0.11
LinearReg 6 - JMA 42 - 0.08
SMA 2 - JMA 42 - 0.11
SMA 6 - JMA 42 - 0.11
SMA 8 - JMA 42 - 0.11
SMMA 12 - JMA 42 - 0.11
SMMA 4 - JMA 42 - 0.11
SMMA 6 - JMA 42 - 0.10
EMA 14 - JMA 44 - 0.11
EMA 16 - JMA 44 - 0.11
EMA 2 - JMA 44 - 0.11
EMA 6 - JMA 44 - 0.11
EMA 8 - JMA 44 - 0.11
HMA 10 - JMA 44 - 0.08
HMA 6 - JMA 44 - 0.11
KAMA 16 - JMA 44 - 0.11
KAMA 18 - JMA 44 - 0.11
KAMA 8 - JMA 44 - 0.11
LinearReg 12 - JMA 44 - 0.11
LinearReg 18 - JMA 44 - 0.05
Lowest 2 - JMA 44 - 0.11
Lowest 4 - JMA 44 - 0.09
Lowest 8 - JMA 44 - 0.10
SMA 14 - JMA 44 - 0.11
SMA 24 - JMA 44 - 0.11
SMA 32 - JMA 44 - 0.11
SMA 4 - JMA 44 - 0.11
SMMA 14 - JMA 44 - 0.11
EMA 18 - JMA 46 - 0.11
HMA 12 - JMA 46 - 0.08
HMA 8 - JMA 46 - 0.11
KAMA 16 - JMA 46 - 0.11
KAMA 2 - JMA 46 - 0.11
KAMA 4 - JMA 46 - 0.11
KAMA 6 - JMA 46 - 0.10
LinearReg 14 - JMA 46 - 0.10
LinearReg 20 - JMA 46 - 0.05
LinearReg 22 - JMA 46 - 0.11
LinearReg 28 - JMA 46 - 0.11
LinearReg 36 - JMA 46 - 0.11
EMA 10 - LinearReg 28 - 0.11
EMA 12 - LinearReg 28 - 0.11
EMA 24 - LinearReg 28 - 0.11
EMA 26 - LinearReg 28 - 0.11
EMA 28 - LinearReg 28 - 0.11
Highest 12 - LinearReg 28 - 0.11
Highest 14 - LinearReg 28 - 0.11
Highest 18 - LinearReg 28 - 0.11
Highest 22 - LinearReg 28 - 0.11
Highest 24 - LinearReg 28 - 0.11
Highest 28 - LinearReg 28 - 0.11
KAMA 22 - LinearReg 28 - 0.11
KAMA 24 - LinearReg 28 - 0.11
KAMA 4 - LinearReg 28 - 0.10
KAMA 6 - LinearReg 28 - 0.11
SMA 18 - LinearReg 28 - 0.11
SMA 26 - LinearReg 28 - 0.11
SMA 28 - LinearReg 28 - 0.11
SMMA 10 - LinearReg 28 - 0.11
SMMA 12 - LinearReg 28 - 0.11
SMMA 18 - LinearReg 28 - 0.11
SMMA 8 - LinearReg 28 - 0.11
EMA 10 - LinearReg 30 - 0.11
EMA 12 - LinearReg 30 - 0.11
EMA 22 - LinearReg 30 - 0.11
Highest 12 - LinearReg 30 - 0.11
Highest 14 - LinearReg 30 - 0.11
Highest 18 - LinearReg 30 - 0.11
Highest 30 - LinearReg 30 - 0.11
Highest 6 - LinearReg 30 - 0.11
JMA 26 - LinearReg 30 - 0.11
JMA 28 - LinearReg 30 - 0.11
KAMA 16 - LinearReg 30 - 0.11
KAMA 18 - LinearReg 30 - 0.11
KAMA 6 - LinearReg 30 - 0.11
SMA 18 - LinearReg 30 - 0.11
SMA 26 - LinearReg 30 - 0.11
SMA 28 - LinearReg 30 - 0.11
SMMA 10 - LinearReg 30 - 0.11
SMMA 12 - LinearReg 30 - 0.11
SMMA 8 - LinearReg 30 - 0.11
EMA 10 - LinearReg 32 - 0.11
EMA 12 - LinearReg 32 - 0.11
EMA 22 - LinearReg 32 - 0.11
Highest 30 - LinearReg 32 - 0.11
Highest 6 - LinearReg 32 - 0.11
JMA 18 - LinearReg 32 - 0.11
KAMA 8 - JMA 42 - 0.11
LinearReg 12 - JMA 42 - 0.11
LinearReg 18 - JMA 42 - 0.05
LinearReg 34 - JMA 42 - 0.11
LinearReg 42 - JMA 42 - 0.11
Lowest 6 - JMA 42 - 0.11
SMA 14 - JMA 42 - 0.10
SMA 24 - JMA 42 - 0.11
SMA 32 - JMA 42 - 0.11
SMA 4 - JMA 42 - 0.06
SMMA 14 - JMA 42 - 0.11
EMA 22 - JMA 44 - 0.11
EMA 24 - JMA 44 - 0.11
EMA 26 - JMA 44 - 0.11
EMA 30 - JMA 44 - 0.11
EMA 4 - JMA 44 - 0.08
HMA 8 - JMA 44 - 0.11
KAMA 14 - JMA 44 - 0.11
LinearReg 2 - JMA 44 - 0.11
LinearReg 28 - JMA 44 - 0.11
LinearReg 38 - JMA 44 - 0.11
LinearReg 40 - JMA 44 - 0.11
LinearReg 6 - JMA 44 - 0.11
SMA 2 - JMA 44 - 0.11
SMA 6 - JMA 44 - 0.10
SMA 8 - JMA 44 - 0.11
SMMA 12 - JMA 44 - 0.11
SMMA 4 - JMA 44 - 0.11
SMMA 6 - JMA 44 - 0.08
EMA 12 - JMA 46 - 0.11
EMA 24 - JMA 46 - 0.11
EMA 26 - JMA 46 - 0.11
HMA 14 - JMA 46 - 0.03
KAMA 12 - JMA 46 - 0.10
KAMA 14 - JMA 46 - 0.10
LinearReg 10 - JMA 46 - 0.06
LinearReg 16 - JMA 46 - 0.07
LinearReg 24 - JMA 46 - 0.11
LinearReg 40 - JMA 46 - 0.11
LinearReg 46 - JMA 46 - 0.11
LinearReg 6 - JMA 46 - 0.10
LinearReg 8 - JMA 46 - 0.09
SMA 14 - JMA 46 - 0.11
SMA 16 - JMA 46 - 0.11
SMA 18 - JMA 46 - 0.11
SMA 24 - JMA 46 - 0.11
SMA 26 - JMA 46 - 0.11
SMA 28 - JMA 46 - 0.11
SMMA 10 - JMA 46 - 0.11
SMMA 20 - JMA 46 - 0.11
LinearReg 44 - JMA 46 - 0.11
Lowest 2 - JMA 46 - 0.11
Lowest 4 - JMA 46 - 0.06
SMA 10 - JMA 46 - 0.11
SMA 20 - JMA 46 - 0.11
SMA 32 - JMA 46 - 0.11
SMMA 2 - JMA 46 - 0.09
EMA 2 - JMA 6 - 0.00
EMA 6 - JMA 6 - 0.00
SMA 4 - JMA 6 - 0.03
HMA 4 - JMA 8 - 0.08
KAMA 2 - JMA 8 - 0.11
LinearReg 2 - JMA 8 - 0.11
SMA 2 - JMA 8 - 0.10
SMA 6 - JMA 8 - 0.00
Highest 10 - LinearReg 10 - 0.11
JMA 10 - LinearReg 10 - 0.11
KAMA 8 - LinearReg 10 - 0.09
EMA 12 - LinearReg 12 - 0.10
EMA 4 - LinearReg 12 - 0.11
Highest 12 - LinearReg 12 - 0.11
JMA 10 - LinearReg 12 - 0.11
KAMA 12 - LinearReg 12 - 0.10
SMA 8 - LinearReg 12 - 0.10
SMMA 10 - LinearReg 12 - 0.10
SMMA 12 - LinearReg 12 - 0.10
SMMA 6 - LinearReg 12 - 0.11
EMA 10 - LinearReg 14 - 0.11
HMA 12 - LinearReg 14 - 0.11
HMA 8 - LinearReg 14 - 0.11
JMA 8 - LinearReg 14 - 0.11
KAMA 2 - LinearReg 14 - 0.11
KAMA 4 - LinearReg 14 - 0.11
KAMA 6 - LinearReg 14 - 0.10
SMA 10 - LinearReg 14 - 0.08
SMMA 2 - LinearReg 14 - 0.11
EMA 14 - LinearReg 16 - 0.09
EMA 16 - LinearReg 16 - 0.09
EMA 6 - LinearReg 16 - 0.11
EMA 8 - LinearReg 16 - 0.11
Highest 16 - LinearReg 16 - 0.11
HMA 10 - LinearReg 16 - 0.11
KAMA 16 - LinearReg 16 - 0.11
KAMA 8 - LinearReg 16 - 0.10
SMA 14 - LinearReg 16 - 0.10
SMA 4 - LinearReg 16 - 0.11
SMMA 14 - LinearReg 16 - 0.09
EMA 12 - LinearReg 18 - 0.11
EMA 14 - LinearReg 18 - 0.11
JMA 26 - LinearReg 32 - 0.11
JMA 28 - LinearReg 32 - 0.09
KAMA 16 - LinearReg 32 - 0.11
KAMA 18 - LinearReg 32 - 0.11
KAMA 8 - LinearReg 32 - 0.11
SMA 10 - LinearReg 32 - 0.11
Highest 26 - LinearReg 34 - 0.11
Highest 34 - LinearReg 34 - 0.11
Highest 8 - LinearReg 34 - 0.11
JMA 30 - LinearReg 34 - 0.11
JMA 32 - LinearReg 34 - 0.07
JMA 34 - LinearReg 34 - 0.07
KAMA 10 - LinearReg 34 - 0.11
KAMA 12 - LinearReg 34 - 0.11
KAMA 14 - LinearReg 34 - 0.11
KAMA 26 - LinearReg 34 - 0.10
SMA 12 - LinearReg 34 - 0.11
SMA 22 - LinearReg 34 - 0.11
SMA 30 - LinearReg 34 - 0.11
Highest 10 - LinearReg 36 - 0.11
Highest 20 - LinearReg 36 - 0.11
Highest 22 - LinearReg 36 - 0.11
Highest 28 - LinearReg 36 - 0.11
SMA 14 - LinearReg 36 - 0.11
SMA 16 - LinearReg 36 - 0.11
SMA 18 - LinearReg 36 - 0.11
SMA 24 - LinearReg 36 - 0.11
SMA 26 - LinearReg 36 - 0.11
Highest 10 - LinearReg 38 - 0.11
Highest 12 - LinearReg 38 - 0.11
SMA 16 - LinearReg 38 - 0.11
SMA 18 - LinearReg 38 - 0.11
EMA 4 - LinearReg 4 - 0.03
EMA 2 - LinearReg 6 - 0.11
EMA 6 - LinearReg 6 - 0.03
Highest 2 - LinearReg 6 - 0.11
Highest 4 - LinearReg 6 - 0.11
Highest 6 - LinearReg 6 - 0.11
HMA 4 - LinearReg 6 - 0.11
JMA 4 - LinearReg 6 - 0.11
SMA 4 - LinearReg 6 - 0.03
EMA 6 - LinearReg 8 - 0.11
Highest 2 - LinearReg 8 - 0.11
Highest 6 - LinearReg 8 - 0.11
HMA 8 - Lowest 10 - 0.06
LinearReg 4 - Lowest 10 - 0.11
EMA 2 - Lowest 12 - 0.01
HMA 4 - Lowest 12 - 0.04
HMA 8 - Lowest 12 - 0.06
EMA 2 - Lowest 14 - 0.00
HMA 10 - Lowest 14 - 0.06
HMA 4 - Lowest 14 - 0.04
LinearReg 14 - Lowest 14 - 0.11
SMMA 12 - Lowest 14 - 0.11
HMA 6 - Lowest 16 - 0.04
LinearReg 12 - Lowest 16 - 0.11
LinearReg 4 - Lowest 16 - 0.11
LinearReg 6 - Lowest 16 - 0.11
LinearReg 8 - Lowest 16 - 0.11
SMMA 14 - Lowest 16 - 0.11
SMMA 16 - Lowest 16 - 0.11
HMA 6 - Lowest 18 - 0.04
LinearReg 10 - Lowest 18 - 0.11
LinearReg 12 - Lowest 18 - 0.11
LinearReg 18 - Lowest 18 - 0.11
LinearReg 4 - Lowest 18 - 0.11
LinearReg 6 - Lowest 18 - 0.11
LinearReg 8 - Lowest 18 - 0.11
SMMA 14 - Lowest 18 - 0.11
SMMA 16 - Lowest 18 - 0.11
LinearReg 14 - Lowest 20 - 0.11
LinearReg 14 - Lowest 22 - 0.11
LinearReg 6 - Lowest 22 - 0.11
HMA 6 - Lowest 24 - 0.04
LinearReg 4 - Lowest 24 - 0.11
LinearReg 6 - Lowest 24 - 0.11
LinearReg 8 - Lowest 24 - 0.11
SMMA 22 - Lowest 24 - 0.10
LinearReg 14 - Lowest 26 - 0.11
LinearReg 20 - Lowest 26 - 0.11
LinearReg 22 - Lowest 26 - 0.11
LinearReg 26 - Lowest 26 - 0.11
SMMA 2 - Lowest 26 - 0.00
LinearReg 20 - Lowest 28 - 0.11
LinearReg 26 - Lowest 28 - 0.11
SMMA 2 - Lowest 28 - 0.00
EMA 2 - Lowest 30 - 0.00
LinearReg 12 - Lowest 30 - 0.11
LinearReg 18 - Lowest 30 - 0.11
EMA 2 - Lowest 32 - 0.00
LinearReg 12 - Lowest 32 - 0.11
LinearReg 18 - Lowest 32 - 0.11
LinearReg 26 - Lowest 32 - 0.11
LinearReg 20 - Lowest 34 - 0.11
LinearReg 26 - Lowest 34 - 0.11
LinearReg 34 - Lowest 34 - 0.11
SMMA 2 - Lowest 34 - 0.00
LinearReg 10 - Lowest 36 - 0.11
LinearReg 12 - Lowest 36 - 0.10
LinearReg 16 - Lowest 36 - 0.11
LinearReg 24 - Lowest 36 - 0.11
LinearReg 32 - Lowest 36 - 0.11
LinearReg 10 - Lowest 38 - 0.11
LinearReg 16 - Lowest 38 - 0.11
LinearReg 24 - Lowest 38 - 0.11
LinearReg 30 - Lowest 38 - 0.11
LinearReg 32 - Lowest 38 - 0.11
LinearReg 36 - Lowest 38 - 0.11
LinearReg 38 - Lowest 38 - 0.11
EMA 2 - Lowest 4 - 0.06
LinearReg 34 - Lowest 40 - 0.11
LinearReg 16 - Lowest 42 - 0.11
LinearReg 24 - Lowest 42 - 0.11
LinearReg 24 - Lowest 44 - 0.11
LinearReg 22 - Lowest 46 - 0.11
LinearReg 28 - Lowest 46 - 0.11
LinearReg 34 - Lowest 46 - 0.11
LinearReg 40 - Lowest 46 - 0.11
LinearReg 42 - Lowest 46 - 0.11
EMA 4 - Lowest 6 - 0.09
HMA 6 - Lowest 6 - 0.08
SMMA 6 - Lowest 6 - 0.11
HMA 6 - Lowest 8 - 0.08
LinearReg 6 - Lowest 8 - 0.11
SMMA 4 - Lowest 8 - 0.11
SMMA 6 - Lowest 8 - 0.11
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.09
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.09
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.08
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - Momentum 10 - 0.03
MeanDeviatio - Momentum 10 - 0.01
Momentum 2 - Momentum 10 - 0.11
Momentum 6 - Momentum 12 - 0.09
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
EMA 16 - LinearReg 18 - 0.11
EMA 6 - LinearReg 18 - 0.11
Highest 10 - LinearReg 18 - 0.11
Highest 16 - LinearReg 18 - 0.11
Highest 6 - LinearReg 18 - 0.11
JMA 16 - LinearReg 18 - 0.11
JMA 18 - LinearReg 18 - 0.11
KAMA 16 - LinearReg 18 - 0.11
KAMA 18 - LinearReg 18 - 0.11
KAMA 8 - LinearReg 18 - 0.11
SMA 14 - LinearReg 18 - 0.09
SMA 16 - LinearReg 18 - 0.09
KAMA 2 - LinearReg 2 - 0.11
SMA 2 - LinearReg 2 - 0.11
EMA 20 - LinearReg 20 - 0.11
KAMA 6 - LinearReg 20 - 0.11
SMA 10 - LinearReg 20 - 0.11
SMA 20 - LinearReg 20 - 0.11
SMA 6 - LinearReg 20 - 0.11
SMMA 16 - LinearReg 20 - 0.10
SMMA 4 - LinearReg 20 - 0.11
EMA 20 - LinearReg 22 - 0.11
EMA 8 - LinearReg 22 - 0.11
KAMA 6 - LinearReg 22 - 0.11
SMA 12 - LinearReg 22 - 0.04
SMA 22 - LinearReg 22 - 0.09
SMMA 14 - LinearReg 22 - 0.11
SMMA 16 - LinearReg 22 - 0.11
SMMA 8 - LinearReg 22 - 0.11
EMA 14 - LinearReg 24 - 0.10
EMA 16 - LinearReg 24 - 0.11
EMA 18 - LinearReg 24 - 0.11
Highest 10 - LinearReg 24 - 0.11
Highest 18 - LinearReg 24 - 0.11
Highest 20 - LinearReg 24 - 0.11
Highest 22 - LinearReg 24 - 0.11
Highest 6 - LinearReg 24 - 0.11
Highest 8 - LinearReg 24 - 0.11
JMA 22 - LinearReg 24 - 0.11
KAMA 10 - LinearReg 24 - 0.11
KAMA 16 - LinearReg 24 - 0.11
KAMA 18 - LinearReg 24 - 0.11
KAMA 20 - LinearReg 24 - 0.11
KAMA 22 - LinearReg 24 - 0.11
KAMA 8 - LinearReg 24 - 0.11
SMA 14 - LinearReg 24 - 0.10
SMA 16 - LinearReg 24 - 0.11
HMA 4 - JMA 6 - 0.11
KAMA 2 - JMA 6 - 0.07
LinearReg 2 - JMA 6 - 0.11
SMA 2 - JMA 6 - 0.11
SMA 6 - JMA 6 - 0.03
SMMA 4 - JMA 6 - 0.02
EMA 2 - JMA 8 - 0.03
EMA 6 - JMA 8 - 0.04
HMA 10 - KAMA 10 - 0.11
SMMA 6 - KAMA 6 - 0.11
HMA 8 - LinearReg 10 - 0.11
JMA 6 - LinearReg 10 - 0.11
JMA 8 - LinearReg 10 - 0.11
KAMA 2 - LinearReg 10 - 0.10
KAMA 6 - LinearReg 10 - 0.10
SMA 10 - LinearReg 10 - 0.11
SMA 2 - LinearReg 10 - 0.11
EMA 6 - LinearReg 12 - 0.11
HMA 10 - LinearReg 12 - 0.11
HMA 12 - LinearReg 12 - 0.06
SMMA 2 - LinearReg 12 - 0.11
SMMA 8 - LinearReg 12 - 0.10
EMA 14 - LinearReg 14 - 0.09
Highest 10 - LinearReg 14 - 0.11
Highest 12 - LinearReg 14 - 0.11
Highest 2 - LinearReg 14 - 0.11
Highest 4 - LinearReg 14 - 0.11
Highest 6 - LinearReg 14 - 0.11
Highest 8 - LinearReg 14 - 0.11
HMA 14 - LinearReg 14 - 0.11
JMA 12 - LinearReg 14 - 0.11
JMA 14 - LinearReg 14 - 0.11
KAMA 10 - LinearReg 14 - 0.09
LinearReg 12 - LinearReg 14 - 0.11
SMA 14 - LinearReg 14 - 0.07
SMMA 10 - LinearReg 14 - 0.09
EMA 4 - LinearReg 16 - 0.11
Highest 14 - LinearReg 16 - 0.11
JMA 10 - LinearReg 16 - 0.11
KAMA 14 - LinearReg 16 - 0.11
SMA 6 - LinearReg 16 - 0.11
SMA 8 - LinearReg 16 - 0.11
SMMA 12 - LinearReg 16 - 0.09
SMMA 4 - LinearReg 16 - 0.11
SMMA 6 - LinearReg 16 - 0.08
SMA 10 - LinearReg 18 - 0.11
SMA 6 - LinearReg 18 - 0.11
SMA 8 - LinearReg 18 - 0.11
Momentum 4 - Momentum 14 - 0.11
Momentum 8 - Momentum 14 - 0.11
MeanDeviatio - Momentum 16 - 0.10
MeanDeviatio - Momentum 18 - 0.09
MeanDeviatio - Momentum 18 - 0.05
Momentum 6 - Momentum 18 - 0.10
MeanDeviatio - Momentum 20 - 0.10
MeanDeviatio - Momentum 20 - 0.09
Momentum 2 - Momentum 20 - 0.11
MeanDeviatio - Momentum 22 - 0.10
Momentum 6 - Momentum 22 - 0.11
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 26 - 0.10
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.10
Momentum 26 - Momentum 28 - 0.11
Momentum 6 - Momentum 28 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
Momentum 10 - Momentum 30 - 0.11
Momentum 12 - Momentum 30 - 0.11
Momentum 20 - Momentum 30 - 0.11
Momentum 26 - Momentum 30 - 0.11
Momentum 8 - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
Momentum 10 - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
Momentum 2 - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
SMA 22 - JMA 22 - 0.08
SMA 6 - JMA 22 - 0.11
SMA 8 - JMA 22 - 0.10
SMMA 14 - JMA 22 - 0.10
SMMA 22 - JMA 22 - 0.11
SMMA 4 - JMA 22 - 0.09
SMMA 6 - JMA 22 - 0.11
EMA 10 - JMA 24 - 0.08
EMA 2 - JMA 24 - 0.11
EMA 22 - JMA 24 - 0.10
EMA 24 - JMA 24 - 0.11
EMA 6 - JMA 24 - 0.11
EMA 8 - JMA 24 - 0.10
HMA 10 - JMA 24 - 0.09
HMA 16 - JMA 24 - 0.09
HMA 4 - JMA 24 - 0.11
KAMA 16 - JMA 24 - 0.11
KAMA 8 - JMA 24 - 0.11
LinearReg 18 - JMA 24 - 0.07
SMA 4 - JMA 24 - 0.10
SMMA 12 - JMA 24 - 0.10
SMMA 24 - JMA 24 - 0.11
EMA 12 - JMA 26 - 0.09
EMA 24 - JMA 26 - 0.11
EMA 26 - JMA 26 - 0.11
HMA 12 - JMA 26 - 0.10
KAMA 2 - JMA 26 - 0.11
KAMA 6 - JMA 26 - 0.11
LinearReg 14 - JMA 26 - 0.06
LinearReg 22 - JMA 26 - 0.05
SMA 14 - JMA 26 - 0.09
SMA 16 - JMA 26 - 0.08
SMA 18 - JMA 26 - 0.08
SMA 2 - JMA 26 - 0.11
SMA 24 - JMA 26 - 0.08
SMA 26 - JMA 26 - 0.11
SMA 6 - JMA 26 - 0.11
SMMA 18 - JMA 26 - 0.11
SMMA 4 - JMA 26 - 0.10
HMA 14 - JMA 28 - 0.10
HMA 8 - JMA 28 - 0.10
KAMA 12 - JMA 28 - 0.11
KAMA 14 - JMA 28 - 0.11
KAMA 26 - JMA 28 - 0.11
KAMA 8 - JMA 28 - 0.11
LinearReg 10 - JMA 28 - 0.11
LinearReg 12 - JMA 28 - 0.09
LinearReg 16 - JMA 28 - 0.05
LinearReg 24 - JMA 28 - 0.04
SMA 10 - JMA 28 - 0.06
SMA 20 - JMA 28 - 0.11
SMA 18 - LinearReg 24 - 0.11
SMA 24 - LinearReg 24 - 0.11
SMMA 20 - LinearReg 24 - 0.11
EMA 10 - LinearReg 26 - 0.11
EMA 24 - LinearReg 26 - 0.11
EMA 26 - LinearReg 26 - 0.11
Highest 14 - LinearReg 26 - 0.11
JMA 20 - LinearReg 26 - 0.11
KAMA 12 - LinearReg 26 - 0.11
KAMA 14 - LinearReg 26 - 0.11
SMA 8 - LinearReg 26 - 0.11
SMMA 12 - LinearReg 26 - 0.11
SMMA 18 - LinearReg 26 - 0.11
SMMA 22 - LinearReg 26 - 0.11
SMMA 6 - LinearReg 26 - 0.11
EMA 22 - LinearReg 28 - 0.11
KAMA 14 - LinearReg 28 - 0.11
SMA 8 - LinearReg 28 - 0.11
SMMA 4 - LinearReg 28 - 0.10
SMMA 6 - LinearReg 28 - 0.11
EMA 20 - LinearReg 30 - 0.11
Highest 26 - LinearReg 30 - 0.11
KAMA 26 - LinearReg 30 - 0.11
SMA 10 - LinearReg 30 - 0.11
SMA 30 - LinearReg 30 - 0.11
SMA 8 - LinearReg 30 - 0.09
SMMA 6 - LinearReg 30 - 0.11
EMA 20 - LinearReg 32 - 0.11
KAMA 26 - LinearReg 32 - 0.11
KAMA 30 - LinearReg 32 - 0.11
KAMA 6 - LinearReg 32 - 0.11
SMA 12 - LinearReg 32 - 0.11
SMA 20 - LinearReg 32 - 0.11
SMA 22 - LinearReg 32 - 0.11
SMA 30 - LinearReg 32 - 0.11
SMMA 8 - LinearReg 32 - 0.11
EMA 18 - LinearReg 34 - 0.11
Highest 10 - LinearReg 34 - 0.11
Highest 16 - LinearReg 34 - 0.11
Highest 20 - LinearReg 34 - 0.11
Highest 28 - LinearReg 34 - 0.11
Highest 30 - LinearReg 34 - 0.11
Highest 32 - LinearReg 34 - 0.11
Highest 6 - LinearReg 34 - 0.11
JMA 22 - LinearReg 34 - 0.11
JMA 24 - LinearReg 34 - 0.11
JMA 26 - LinearReg 34 - 0.11
SMMA 4 - LinearReg 18 - 0.11
EMA 2 - LinearReg 2 - 0.06
EMA 12 - LinearReg 20 - 0.09
EMA 14 - LinearReg 20 - 0.11
EMA 16 - LinearReg 20 - 0.11
EMA 6 - LinearReg 20 - 0.11
Highest 10 - LinearReg 20 - 0.11
Highest 16 - LinearReg 20 - 0.11
Highest 4 - LinearReg 20 - 0.11
Highest 8 - LinearReg 20 - 0.11
JMA 14 - LinearReg 20 - 0.11
JMA 20 - LinearReg 20 - 0.11
KAMA 10 - LinearReg 20 - 0.11
KAMA 12 - LinearReg 20 - 0.11
KAMA 14 - LinearReg 20 - 0.11
KAMA 8 - LinearReg 20 - 0.11
SMA 14 - LinearReg 20 - 0.09
SMA 16 - LinearReg 20 - 0.09
SMA 18 - LinearReg 20 - 0.09
SMMA 18 - LinearReg 20 - 0.11
EMA 12 - LinearReg 22 - 0.11
EMA 14 - LinearReg 22 - 0.11
EMA 16 - LinearReg 22 - 0.11
Highest 12 - LinearReg 22 - 0.11
Highest 14 - LinearReg 22 - 0.11
Highest 18 - LinearReg 22 - 0.11
Highest 8 - LinearReg 22 - 0.11
JMA 20 - LinearReg 22 - 0.11
KAMA 10 - LinearReg 22 - 0.11
KAMA 12 - LinearReg 22 - 0.11
KAMA 14 - LinearReg 22 - 0.11
SMA 18 - LinearReg 22 - 0.11
SMMA 10 - LinearReg 22 - 0.11
SMMA 12 - LinearReg 22 - 0.11
SMMA 18 - LinearReg 22 - 0.11
SMMA 20 - LinearReg 22 - 0.11
SMMA 22 - LinearReg 22 - 0.11
SMMA 6 - LinearReg 22 - 0.11
EMA 10 - LinearReg 24 - 0.11
EMA 20 - LinearReg 24 - 0.11
EMA 22 - LinearReg 24 - 0.10
KAMA 6 - LinearReg 24 - 0.11
SMA 10 - LinearReg 24 - 0.11
SMA 20 - LinearReg 24 - 0.11
EMA 14 - LinearReg 26 - 0.11
EMA 16 - LinearReg 26 - 0.10
EMA 8 - LinearReg 26 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
Momentum 4 - Momentum 38 - 0.11
Momentum 8 - Momentum 38 - 0.11
MeanDeviatio - Momentum 4 - 0.02
Momentum 2 - Momentum 4 - 0.00
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
Momentum 4 - Momentum 40 - 0.10
Momentum 8 - Momentum 40 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
Momentum 6 - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
Momentum 6 - Momentum 46 - 0.11
MeanDeviatio - Momentum 6 - 0.03
Momentum 4 - Momentum 6 - 0.11
Momentum 6 - Momentum 8 - 0.11
SMMA 10 - SMA 10 - 0.11
SMMA 8 - SMA 12 - 0.11
HMA 6 - SMA 14 - 0.09
HMA 6 - SMA 16 - 0.11
SMMA 14 - SMA 16 - 0.11
HMA 10 - SMA 20 - 0.03
HMA 12 - SMA 20 - 0.03
HMA 14 - SMA 24 - 0.04
HMA 14 - SMA 32 - 0.02
HMA 10 - SMA 34 - 0.11
HMA 14 - SMA 36 - 0.03
HMA 10 - SMA 38 - 0.11
Highest 2 - SMA 4 - 0.11
HMA 10 - SMA 42 - 0.11
HMA 12 - SMA 42 - 0.03
HMA 12 - SMA 46 - 0.03
SMMA 6 - SMA 6 - 0.11
KAMA 8 - SMA 8 - 0.11
HMA 6 - SMMA 10 - 0.07
HMA 6 - SMMA 14 - 0.03
Highest 2 - SMMA 2 - 0.11
HMA 12 - SMMA 20 - 0.02
HMA 12 - SMMA 24 - 0.02
HMA 12 - SMMA 28 - 0.03
SMMA 16 - JMA 28 - 0.11
SMMA 20 - JMA 28 - 0.11
EMA 18 - JMA 30 - 0.11
EMA 4 - JMA 30 - 0.11
HMA 6 - JMA 30 - 0.11
KAMA 12 - JMA 30 - 0.11
KAMA 14 - JMA 30 - 0.11
KAMA 26 - JMA 30 - 0.10
LinearReg 10 - JMA 30 - 0.11
LinearReg 16 - JMA 30 - 0.11
LinearReg 6 - JMA 30 - 0.11
Lowest 2 - JMA 30 - 0.11
Lowest 4 - JMA 30 - 0.11
Lowest 8 - JMA 30 - 0.04
SMA 14 - JMA 30 - 0.11
SMA 2 - JMA 30 - 0.11
SMA 22 - JMA 30 - 0.10
SMA 6 - JMA 30 - 0.11
SMA 8 - JMA 30 - 0.11
SMMA 14 - JMA 30 - 0.11
SMMA 22 - JMA 30 - 0.11
SMMA 4 - JMA 30 - 0.11
SMMA 6 - JMA 30 - 0.11
EMA 14 - JMA 32 - 0.11
EMA 16 - JMA 32 - 0.08
EMA 28 - JMA 32 - 0.11
EMA 4 - JMA 32 - 0.11
HMA 6 - JMA 32 - 0.11
KAMA 12 - JMA 32 - 0.10
KAMA 14 - JMA 32 - 0.11
LinearReg 6 - JMA 32 - 0.11
LinearReg 8 - JMA 32 - 0.11
Lowest 6 - JMA 32 - 0.11
SMA 12 - JMA 32 - 0.11
SMA 2 - JMA 32 - 0.11
SMA 22 - JMA 32 - 0.11
SMA 32 - JMA 32 - 0.10
SMA 6 - JMA 32 - 0.11
SMA 8 - JMA 32 - 0.11
SMMA 14 - JMA 32 - 0.11
SMMA 22 - JMA 32 - 0.11
SMMA 4 - JMA 32 - 0.11
SMMA 6 - JMA 32 - 0.11
EMA 10 - JMA 34 - 0.11
EMA 2 - JMA 34 - 0.11
EMA 24 - JMA 34 - 0.10
EMA 26 - JMA 34 - 0.10
EMA 30 - JMA 34 - 0.11
EMA 6 - JMA 34 - 0.11
EMA 8 - JMA 34 - 0.11
HMA 10 - JMA 34 - 0.11
JMA 28 - LinearReg 34 - 0.11
KAMA 16 - LinearReg 34 - 0.11
KAMA 18 - LinearReg 34 - 0.11
KAMA 28 - LinearReg 34 - 0.11
SMA 14 - LinearReg 34 - 0.11
SMA 16 - LinearReg 34 - 0.11
SMA 24 - LinearReg 34 - 0.11
SMA 26 - LinearReg 34 - 0.11
SMA 32 - LinearReg 34 - 0.11
Highest 12 - LinearReg 36 - 0.11
Highest 14 - LinearReg 36 - 0.11
Highest 18 - LinearReg 36 - 0.11
Highest 24 - LinearReg 36 - 0.11
Highest 34 - LinearReg 36 - 0.11
Highest 8 - LinearReg 36 - 0.11
JMA 30 - LinearReg 36 - 0.11
JMA 32 - LinearReg 36 - 0.07
JMA 34 - LinearReg 36 - 0.07
Highest 14 - LinearReg 38 - 0.11
KAMA 2 - LinearReg 4 - 0.11
SMA 2 - LinearReg 4 - 0.11
SMMA 4 - LinearReg 4 - 0.03
KAMA 2 - LinearReg 6 - 0.03
KAMA 4 - LinearReg 6 - 0.10
KAMA 6 - LinearReg 6 - 0.10
SMMA 2 - LinearReg 6 - 0.11
EMA 2 - Lowest 10 - 0.07
HMA 10 - Lowest 10 - 0.06
HMA 6 - Lowest 10 - 0.06
SMMA 8 - Lowest 10 - 0.11
HMA 10 - Lowest 12 - 0.06
HMA 12 - Lowest 12 - 0.09
HMA 6 - Lowest 12 - 0.04
SMMA 2 - Lowest 12 - 0.01
SMMA 8 - Lowest 12 - 0.11
HMA 2 - Lowest 14 - 0.11
LinearReg 12 - Lowest 14 - 0.11
LinearReg 4 - Lowest 14 - 0.11
LinearReg 8 - Lowest 14 - 0.11
SMMA 10 - Lowest 14 - 0.11
EMA 2 - Lowest 16 - 0.00
HMA 10 - Lowest 16 - 0.06
HMA 8 - Lowest 18 - 0.06
LinearReg 16 - Lowest 18 - 0.11
EMA 2 - Lowest 2 - 0.11
LinearReg 4 - Lowest 20 - 0.11
LinearReg 6 - Lowest 20 - 0.11
LinearReg 8 - Lowest 20 - 0.11
Highest 16 - LinearReg 26 - 0.11
Highest 20 - LinearReg 26 - 0.11
Highest 26 - LinearReg 26 - 0.11
JMA 26 - LinearReg 26 - 0.11
KAMA 16 - LinearReg 26 - 0.11
KAMA 26 - LinearReg 26 - 0.11
KAMA 8 - LinearReg 26 - 0.11
SMA 12 - LinearReg 26 - 0.11
SMA 22 - LinearReg 26 - 0.11
SMMA 14 - LinearReg 26 - 0.11
SMMA 8 - LinearReg 26 - 0.10
EMA 14 - LinearReg 28 - 0.11
EMA 16 - LinearReg 28 - 0.11
EMA 8 - LinearReg 28 - 0.11
Highest 10 - LinearReg 28 - 0.11
Highest 16 - LinearReg 28 - 0.11
Highest 20 - LinearReg 28 - 0.11
Highest 26 - LinearReg 28 - 0.11
Highest 6 - LinearReg 28 - 0.11
JMA 18 - LinearReg 28 - 0.11
JMA 26 - LinearReg 28 - 0.11
JMA 28 - LinearReg 28 - 0.11
KAMA 16 - LinearReg 28 - 0.11
KAMA 18 - LinearReg 28 - 0.11
KAMA 20 - LinearReg 28 - 0.11
KAMA 28 - LinearReg 28 - 0.11
KAMA 8 - LinearReg 28 - 0.11
SMA 14 - LinearReg 28 - 0.10
SMA 16 - LinearReg 28 - 0.11
SMA 24 - LinearReg 28 - 0.11
SMMA 14 - LinearReg 28 - 0.11
EMA 14 - LinearReg 30 - 0.11
EMA 16 - LinearReg 30 - 0.11
EMA 24 - LinearReg 30 - 0.11
EMA 26 - LinearReg 30 - 0.11
Highest 10 - LinearReg 30 - 0.11
Highest 16 - LinearReg 30 - 0.11
Highest 22 - LinearReg 30 - 0.11
Highest 24 - LinearReg 30 - 0.11
Highest 8 - LinearReg 30 - 0.11
JMA 20 - LinearReg 30 - 0.11
JMA 22 - LinearReg 30 - 0.11
JMA 24 - LinearReg 30 - 0.11
KAMA 10 - LinearReg 30 - 0.11
KAMA 12 - LinearReg 30 - 0.11
KAMA 14 - LinearReg 30 - 0.11
KAMA 22 - LinearReg 30 - 0.11
KAMA 16 - JMA 34 - 0.11
KAMA 8 - JMA 34 - 0.11
LinearReg 12 - JMA 34 - 0.05
LinearReg 18 - JMA 34 - 0.08
LinearReg 26 - JMA 34 - 0.11
SMA 4 - JMA 34 - 0.10
SMMA 12 - JMA 34 - 0.11
SMMA 8 - JMA 34 - 0.11
EMA 12 - JMA 36 - 0.11
EMA 24 - JMA 36 - 0.11
EMA 26 - JMA 36 - 0.11
KAMA 2 - JMA 36 - 0.11
KAMA 6 - JMA 36 - 0.11
LinearReg 14 - JMA 36 - 0.11
LinearReg 22 - JMA 36 - 0.11
LinearReg 28 - JMA 36 - 0.11
SMA 14 - JMA 36 - 0.11
SMA 16 - JMA 36 - 0.10
SMA 18 - JMA 36 - 0.11
SMA 2 - JMA 36 - 0.11
SMA 24 - JMA 36 - 0.11
SMA 26 - JMA 36 - 0.09
SMA 28 - JMA 36 - 0.10
SMA 36 - JMA 36 - 0.11
SMA 6 - JMA 36 - 0.10
SMMA 18 - JMA 36 - 0.11
SMMA 4 - JMA 36 - 0.11
EMA 2 - JMA 38 - 0.11
HMA 14 - JMA 38 - 0.10
HMA 8 - JMA 38 - 0.11
KAMA 12 - JMA 38 - 0.11
KAMA 14 - JMA 38 - 0.11
KAMA 8 - JMA 38 - 0.11
LinearReg 10 - JMA 38 - 0.09
LinearReg 12 - JMA 38 - 0.11
LinearReg 16 - JMA 38 - 0.09
LinearReg 24 - JMA 38 - 0.11
LinearReg 30 - JMA 38 - 0.11
LinearReg 36 - JMA 38 - 0.11
Lowest 4 - JMA 38 - 0.09
SMA 10 - JMA 38 - 0.11
SMA 20 - JMA 38 - 0.10
SMMA 16 - JMA 38 - 0.10
SMMA 20 - JMA 38 - 0.11
EMA 2 - JMA 4 - 0.02
HMA 4 - JMA 4 - 0.11
SMA 4 - JMA 4 - 0.03
EMA 20 - JMA 40 - 0.11
HMA 14 - JMA 40 - 0.09
HMA 6 - JMA 40 - 0.11
SMMA 18 - Lowest 20 - 0.11
LinearReg 10 - Lowest 22 - 0.11
LinearReg 16 - Lowest 22 - 0.11
LinearReg 4 - Lowest 22 - 0.11
LinearReg 8 - Lowest 22 - 0.11
SMMA 18 - Lowest 22 - 0.11
SMMA 20 - Lowest 22 - 0.11
SMMA 22 - Lowest 22 - 0.11
HMA 12 - Lowest 24 - 0.06
LinearReg 14 - Lowest 24 - 0.11
LinearReg 22 - Lowest 24 - 0.11
SMMA 2 - Lowest 24 - 0.00
LinearReg 6 - Lowest 26 - 0.11
EMA 2 - Lowest 28 - 0.00
LinearReg 12 - Lowest 28 - 0.11
LinearReg 18 - Lowest 28 - 0.11
LinearReg 20 - Lowest 30 - 0.11
LinearReg 26 - Lowest 30 - 0.11
LinearReg 30 - Lowest 30 - 0.11
SMMA 2 - Lowest 30 - 0.00
LinearReg 14 - Lowest 32 - 0.11
LinearReg 20 - Lowest 32 - 0.11
LinearReg 22 - Lowest 32 - 0.11
LinearReg 28 - Lowest 32 - 0.11
LinearReg 30 - Lowest 32 - 0.11
LinearReg 32 - Lowest 32 - 0.11
SMMA 2 - Lowest 32 - 0.00
EMA 2 - Lowest 34 - 0.00
LinearReg 10 - Lowest 34 - 0.11
LinearReg 12 - Lowest 34 - 0.11
LinearReg 18 - Lowest 34 - 0.11
EMA 2 - Lowest 36 - 0.00
LinearReg 18 - Lowest 36 - 0.11
LinearReg 20 - Lowest 36 - 0.11
LinearReg 26 - Lowest 36 - 0.11
LinearReg 30 - Lowest 36 - 0.11
LinearReg 36 - Lowest 36 - 0.11
SMMA 2 - Lowest 36 - 0.00
KAMA 4 - Lowest 4 - 0.11
SMMA 2 - Lowest 4 - 0.11
LinearReg 16 - Lowest 40 - 0.11
LinearReg 24 - Lowest 40 - 0.11
LinearReg 40 - Lowest 40 - 0.11
LinearReg 18 - Lowest 42 - 0.11
LinearReg 34 - Lowest 42 - 0.11
LinearReg 40 - Lowest 42 - 0.11
LinearReg 42 - Lowest 42 - 0.11
KAMA 24 - LinearReg 30 - 0.11
KAMA 8 - LinearReg 30 - 0.11
SMA 14 - LinearReg 30 - 0.11
SMA 16 - LinearReg 30 - 0.10
SMA 24 - LinearReg 30 - 0.11
EMA 14 - LinearReg 32 - 0.11
EMA 16 - LinearReg 32 - 0.11
Highest 12 - LinearReg 32 - 0.11
Highest 14 - LinearReg 32 - 0.11
Highest 18 - LinearReg 32 - 0.11
Highest 22 - LinearReg 32 - 0.11
Highest 24 - LinearReg 32 - 0.11
Highest 28 - LinearReg 32 - 0.11
Highest 32 - LinearReg 32 - 0.11
Highest 4 - LinearReg 32 - 0.11
Highest 8 - LinearReg 32 - 0.11
JMA 20 - LinearReg 32 - 0.11
JMA 22 - LinearReg 32 - 0.11
JMA 24 - LinearReg 32 - 0.11
JMA 32 - LinearReg 32 - 0.07
KAMA 10 - LinearReg 32 - 0.11
KAMA 12 - LinearReg 32 - 0.11
KAMA 22 - LinearReg 32 - 0.11
SMA 18 - LinearReg 32 - 0.11
SMA 26 - LinearReg 32 - 0.11
SMA 28 - LinearReg 32 - 0.11
SMMA 10 - LinearReg 32 - 0.11
SMMA 12 - LinearReg 32 - 0.11
SMA 10 - LinearReg 34 - 0.11
SMA 20 - LinearReg 34 - 0.11
Highest 16 - LinearReg 36 - 0.11
Highest 26 - LinearReg 36 - 0.11
Highest 36 - LinearReg 36 - 0.11
SMA 22 - LinearReg 36 - 0.11
Highest 16 - LinearReg 38 - 0.11
JMA 32 - LinearReg 38 - 0.06
JMA 34 - LinearReg 38 - 0.05
JMA 36 - LinearReg 38 - 0.08
JMA 38 - LinearReg 38 - 0.11
EMA 2 - LinearReg 4 - 0.01
KAMA 2 - LinearReg 8 - 0.11
KAMA 4 - LinearReg 8 - 0.10
KAMA 6 - LinearReg 8 - 0.10
SMMA 2 - LinearReg 8 - 0.11
HMA 4 - Lowest 10 - 0.04
LinearReg 10 - Lowest 10 - 0.11
LinearReg 6 - Lowest 10 - 0.11
KAMA 10 - JMA 40 - 0.11
KAMA 12 - JMA 40 - 0.11
LinearReg 12 - JMA 40 - 0.10
LinearReg 18 - JMA 40 - 0.10
LinearReg 24 - JMA 40 - 0.11
LinearReg 30 - JMA 40 - 0.11
LinearReg 32 - JMA 40 - 0.11
LinearReg 36 - JMA 40 - 0.11
LinearReg 4 - JMA 40 - 0.11
LinearReg 8 - JMA 40 - 0.11
SMA 10 - JMA 40 - 0.11
SMA 12 - JMA 40 - 0.11
SMA 20 - JMA 40 - 0.10
SMA 22 - JMA 40 - 0.10
SMA 30 - JMA 40 - 0.10
SMMA 16 - JMA 40 - 0.11
SMMA 20 - JMA 40 - 0.11
SMMA 22 - JMA 40 - 0.11
EMA 18 - JMA 42 - 0.11
HMA 14 - JMA 42 - 0.09
HMA 8 - JMA 42 - 0.11
KAMA 10 - JMA 42 - 0.11
KAMA 12 - JMA 42 - 0.11
KAMA 18 - JMA 42 - 0.11
LinearReg 10 - JMA 42 - 0.08
LinearReg 16 - JMA 42 - 0.09
LinearReg 24 - JMA 42 - 0.11
LinearReg 4 - JMA 42 - 0.11
LinearReg 8 - JMA 42 - 0.09
Lowest 2 - JMA 42 - 0.11
Lowest 4 - JMA 42 - 0.10
Lowest 8 - JMA 42 - 0.11
SMA 10 - JMA 42 - 0.11
SMA 12 - JMA 42 - 0.11
SMA 20 - JMA 42 - 0.09
SMA 22 - JMA 42 - 0.10
SMA 30 - JMA 42 - 0.10
SMMA 16 - JMA 42 - 0.11
SMMA 20 - JMA 42 - 0.11
SMMA 22 - JMA 42 - 0.11
EMA 10 - JMA 44 - 0.11
EMA 12 - JMA 44 - 0.11
EMA 28 - JMA 44 - 0.11
HMA 12 - JMA 44 - 0.09
KAMA 2 - JMA 44 - 0.08
KAMA 4 - JMA 44 - 0.10
KAMA 6 - JMA 44 - 0.11
LinearReg 14 - JMA 44 - 0.11
LinearReg 20 - JMA 44 - 0.10
LinearReg 22 - JMA 44 - 0.10
LinearReg 28 - Lowest 44 - 0.11
LinearReg 30 - Lowest 44 - 0.11
LinearReg 32 - Lowest 44 - 0.11
LinearReg 36 - Lowest 44 - 0.11
LinearReg 44 - Lowest 44 - 0.11
LinearReg 24 - Lowest 46 - 0.11
LinearReg 32 - Lowest 46 - 0.11
LinearReg 38 - Lowest 46 - 0.11
LinearReg 46 - Lowest 46 - 0.11
EMA 2 - Lowest 6 - 0.07
EMA 6 - Lowest 6 - 0.11
HMA 2 - Lowest 6 - 0.08
LinearReg 4 - Lowest 6 - 0.11
LinearReg 6 - Lowest 6 - 0.11
HMA 2 - Lowest 8 - 0.08
LinearReg 4 - Lowest 8 - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.09
MeanDeviatio - Momentum 10 - 0.01
Momentum 6 - Momentum 10 - 0.01
MeanDeviatio - Momentum 12 - 0.04
Momentum 10 - Momentum 12 - 0.11
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
Momentum 10 - Momentum 14 - 0.11
Momentum 12 - Momentum 14 - 0.11
Momentum 6 - Momentum 14 - 0.11
MeanDeviatio - Momentum 16 - 0.09
Momentum 2 - Momentum 16 - 0.11
MeanDeviatio - Momentum 18 - 0.07
MeanDeviatio - Momentum 18 - 0.05
Momentum 2 - Momentum 18 - 0.11
MeanDeviatio - Momentum 20 - 0.09
MeanDeviatio - Momentum 20 - 0.10
Momentum 6 - Momentum 20 - 0.11
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
LinearReg 8 - Lowest 10 - 0.11
SMMA 6 - Lowest 10 - 0.11
HMA 2 - Lowest 12 - 0.11
LinearReg 12 - Lowest 12 - 0.11
LinearReg 4 - Lowest 12 - 0.11
LinearReg 6 - Lowest 12 - 0.11
LinearReg 8 - Lowest 12 - 0.11
SMMA 10 - Lowest 12 - 0.11
SMMA 12 - Lowest 12 - 0.11
HMA 12 - Lowest 14 - 0.06
HMA 8 - Lowest 14 - 0.06
SMMA 2 - Lowest 14 - 0.00
SMMA 8 - Lowest 14 - 0.10
HMA 4 - Lowest 16 - 0.04
HMA 8 - Lowest 16 - 0.06
LinearReg 10 - Lowest 16 - 0.11
LinearReg 16 - Lowest 16 - 0.11
EMA 2 - Lowest 18 - 0.00
HMA 10 - Lowest 20 - 0.05
HMA 12 - Lowest 20 - 0.06
HMA 6 - Lowest 20 - 0.04
LinearReg 18 - Lowest 20 - 0.11
LinearReg 20 - Lowest 20 - 0.11
SMMA 14 - Lowest 20 - 0.07
SMMA 16 - Lowest 20 - 0.10
SMMA 2 - Lowest 20 - 0.00
HMA 12 - Lowest 22 - 0.06
HMA 6 - Lowest 22 - 0.04
LinearReg 20 - Lowest 22 - 0.11
LinearReg 22 - Lowest 22 - 0.11
SMMA 16 - Lowest 22 - 0.10
SMMA 2 - Lowest 22 - 0.00
LinearReg 10 - Lowest 24 - 0.11
LinearReg 16 - Lowest 24 - 0.11
LinearReg 24 - Lowest 24 - 0.11
SMMA 20 - Lowest 24 - 0.07
EMA 2 - Lowest 26 - 0.00
HMA 6 - Lowest 26 - 0.04
LinearReg 12 - Lowest 26 - 0.11
LinearReg 18 - Lowest 26 - 0.11
LinearReg 14 - Lowest 28 - 0.11
LinearReg 22 - Lowest 28 - 0.11
LinearReg 28 - Lowest 28 - 0.11
LinearReg 10 - Lowest 30 - 0.11
LinearReg 16 - Lowest 30 - 0.11
LinearReg 24 - Lowest 30 - 0.11
LinearReg 6 - Lowest 30 - 0.11
LinearReg 8 - Lowest 30 - 0.11
LinearReg 26 - JMA 44 - 0.11
LinearReg 34 - JMA 44 - 0.11
LinearReg 42 - JMA 44 - 0.11
Lowest 6 - JMA 44 - 0.11
SMA 16 - JMA 44 - 0.11
SMA 18 - JMA 44 - 0.10
SMA 26 - JMA 44 - 0.11
SMA 28 - JMA 44 - 0.10
SMA 34 - JMA 44 - 0.11
SMA 36 - JMA 44 - 0.11
SMMA 10 - JMA 44 - 0.11
SMMA 18 - JMA 44 - 0.11
SMMA 2 - JMA 44 - 0.07
SMMA 8 - JMA 44 - 0.11
EMA 14 - JMA 46 - 0.11
EMA 16 - JMA 46 - 0.11
EMA 28 - JMA 46 - 0.11
EMA 4 - JMA 46 - 0.10
HMA 6 - JMA 46 - 0.11
LinearReg 2 - JMA 46 - 0.11
LinearReg 34 - JMA 46 - 0.11
LinearReg 42 - JMA 46 - 0.11
Lowest 6 - JMA 46 - 0.11
Lowest 8 - JMA 46 - 0.11
SMA 12 - JMA 46 - 0.11
SMA 2 - JMA 46 - 0.11
SMA 22 - JMA 46 - 0.11
SMA 34 - JMA 46 - 0.11
SMA 6 - JMA 46 - 0.10
SMA 8 - JMA 46 - 0.11
SMMA 14 - JMA 46 - 0.11
SMMA 18 - JMA 46 - 0.11
SMMA 22 - JMA 46 - 0.11
SMMA 4 - JMA 46 - 0.07
SMMA 6 - JMA 46 - 0.11
EMA 4 - JMA 6 - 0.00
HMA 6 - JMA 6 - 0.11
LinearReg 4 - JMA 6 - 0.11
LinearReg 6 - JMA 6 - 0.11
Lowest 2 - JMA 6 - 0.11
Lowest 4 - JMA 6 - 0.03
Lowest 6 - JMA 6 - 0.02
SMMA 6 - JMA 6 - 0.03
EMA 4 - JMA 8 - 0.02
HMA 6 - JMA 8 - 0.11
LinearReg 4 - JMA 8 - 0.11
LinearReg 6 - JMA 8 - 0.10
LinearReg 8 - JMA 8 - 0.10
Lowest 2 - JMA 8 - 0.11
Lowest 4 - JMA 8 - 0.04
Lowest 6 - JMA 8 - 0.02
Momentum 10 - Momentum 22 - 0.11
Momentum 12 - Momentum 22 - 0.10
Momentum 4 - Momentum 22 - 0.10
Momentum 8 - Momentum 22 - 0.11
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.11
Momentum 10 - Momentum 24 - 0.11
Momentum 6 - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
Momentum 2 - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
Momentum 2 - Momentum 28 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
Momentum 22 - Momentum 30 - 0.11
Momentum 24 - Momentum 30 - 0.11
Momentum 28 - Momentum 30 - 0.11
Momentum 4 - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
Momentum 26 - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
Momentum 26 - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
Momentum 10 - Momentum 36 - 0.11
Momentum 6 - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
Momentum 6 - Momentum 38 - 0.11
MeanDeviatio - Momentum 40 - 0.11
LinearReg 10 - Lowest 32 - 0.11
LinearReg 16 - Lowest 32 - 0.11
LinearReg 24 - Lowest 32 - 0.11
LinearReg 14 - Lowest 34 - 0.11
LinearReg 22 - Lowest 34 - 0.11
LinearReg 28 - Lowest 34 - 0.11
LinearReg 32 - Lowest 34 - 0.11
LinearReg 8 - Lowest 36 - 0.11
EMA 2 - Lowest 38 - 0.00
LinearReg 12 - Lowest 38 - 0.10
LinearReg 18 - Lowest 38 - 0.11
EMA 4 - Lowest 4 - 0.11
HMA 2 - Lowest 4 - 0.10
LinearReg 14 - Lowest 40 - 0.11
LinearReg 20 - Lowest 40 - 0.11
LinearReg 22 - Lowest 40 - 0.11
LinearReg 28 - Lowest 40 - 0.11
LinearReg 36 - Lowest 40 - 0.11
LinearReg 22 - Lowest 42 - 0.11
LinearReg 28 - Lowest 42 - 0.11
LinearReg 30 - Lowest 42 - 0.11
LinearReg 36 - Lowest 42 - 0.11
LinearReg 18 - Lowest 44 - 0.11
LinearReg 34 - Lowest 44 - 0.11
LinearReg 42 - Lowest 44 - 0.11
LinearReg 20 - Lowest 46 - 0.11
LinearReg 26 - Lowest 46 - 0.11
HMA 4 - Lowest 6 - 0.10
SMMA 4 - Lowest 6 - 0.11
HMA 8 - Lowest 8 - 0.06
SMMA 2 - Lowest 8 - 0.03
SMMA 8 - Lowest 8 - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.08
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.08
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - Momentum 10 - 0.02
MeanDeviatio - Momentum 12 - 0.04
Momentum 2 - Momentum 12 - 0.10
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
Momentum 2 - Momentum 40 - 0.11
MeanDeviatio - Momentum 42 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
Momentum 2 - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
Momentum 2 - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
Momentum 10 - Momentum 46 - 0.11
Momentum 4 - Momentum 46 - 0.11
Momentum 8 - Momentum 46 - 0.11
MeanDeviatio - Momentum 6 - 0.03
SMMA 8 - SMA 10 - 0.11
SMMA 10 - SMA 12 - 0.11
SMMA 12 - SMA 12 - 0.11
SMMA 14 - SMA 14 - 0.11
HMA 10 - SMA 18 - 0.03
HMA 8 - SMA 20 - 0.08
HMA 8 - SMA 22 - 0.06
HMA 14 - SMA 30 - 0.03
HMA 12 - SMA 34 - 0.03
HMA 10 - SMA 36 - 0.11
HMA 12 - SMA 36 - 0.03
HMA 12 - SMA 38 - 0.03
HMA 10 - SMA 40 - 0.11
HMA 12 - SMA 40 - 0.03
HMA 14 - SMA 44 - 0.03
KAMA 4 - SMA 6 - 0.11
KAMA 6 - SMA 6 - 0.11
SMMA 8 - SMA 8 - 0.11
HMA 10 - SMMA 18 - 0.03
HMA 10 - SMMA 20 - 0.03
HMA 12 - SMMA 22 - 0.02
Momentum 2 - Momentum 14 - 0.11
MeanDeviatio - Momentum 16 - 0.09
Momentum 10 - Momentum 16 - 0.11
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 18 - 0.09
MeanDeviatio - Momentum 18 - 0.09
Momentum 10 - Momentum 18 - 0.11
Momentum 8 - Momentum 18 - 0.11
MeanDeviatio - Momentum 20 - 0.10
MeanDeviatio - Momentum 20 - 0.05
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.09
MeanDeviatio - Momentum 22 - 0.10
Momentum 2 - Momentum 22 - 0.11
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
Momentum 10 - Momentum 26 - 0.11
Momentum 12 - Momentum 26 - 0.11
Momentum 4 - Momentum 26 - 0.11
Momentum 8 - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
Momentum 10 - Momentum 28 - 0.11
Momentum 12 - Momentum 28 - 0.10
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
Momentum 6 - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
Momentum 6 - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
Momentum 6 - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
Momentum 2 - Momentum 36 - 0.11
Momentum 26 - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
Momentum 26 - Momentum 38 - 0.08
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.11
Momentum 6 - Momentum 40 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
Momentum 4 - Momentum 42 - 0.11
Momentum 8 - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
Momentum 8 - Momentum 44 - 0.10
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
Momentum 2 - Momentum 46 - 0.11
MeanDeviatio - Momentum 8 - 0.01
Momentum 2 - Momentum 8 - 0.11
HMA 10 - SMA 10 - 0.11
HMA 6 - SMA 10 - 0.11
KAMA 10 - SMA 10 - 0.11
KAMA 8 - SMA 10 - 0.11
HMA 12 - SMA 12 - 0.03
SMMA 12 - SMA 14 - 0.11
HMA 8 - SMA 16 - 0.11
HMA 8 - SMA 18 - 0.11
SMMA 2 - SMA 2 - 0.00
HMA 10 - SMA 22 - 0.03
HMA 12 - SMA 22 - 0.03
HMA 12 - SMA 26 - 0.03
HMA 12 - SMA 30 - 0.11
HMA 14 - SMA 34 - 0.02
HMA 14 - SMA 38 - 0.03
HMA 14 - SMA 40 - 0.03
HMA 10 - SMA 44 - 0.11
HMA 12 - SMA 44 - 0.03
HMA 10 - SMA 46 - 0.03
SMMA 6 - SMA 8 - 0.11
HMA 12 - SMMA 12 - 0.03
HMA 6 - SMMA 12 - 0.06
HMA 12 - SMMA 16 - 0.02
Lowest 8 - JMA 8 - 0.00
SMA 8 - JMA 8 - 0.00
SMMA 6 - JMA 8 - 0.09
HMA 12 - KAMA 12 - 0.03
EMA 10 - LinearReg 10 - 0.08
EMA 6 - LinearReg 10 - 0.10
EMA 8 - LinearReg 10 - 0.11
Highest 2 - LinearReg 10 - 0.11
Highest 4 - LinearReg 10 - 0.11
Highest 6 - LinearReg 10 - 0.11
Highest 8 - LinearReg 10 - 0.11
HMA 10 - LinearReg 10 - 0.11
KAMA 10 - LinearReg 10 - 0.10
KAMA 4 - LinearReg 10 - 0.11
SMA 4 - LinearReg 10 - 0.11
SMMA 10 - LinearReg 10 - 0.08
SMMA 2 - LinearReg 10 - 0.11
SMMA 8 - LinearReg 10 - 0.11
EMA 10 - LinearReg 12 - 0.10
Highest 2 - LinearReg 12 - 0.11
Highest 6 - LinearReg 12 - 0.11
Highest 8 - LinearReg 12 - 0.11
KAMA 10 - LinearReg 12 - 0.10
SMA 10 - LinearReg 12 - 0.09
EMA 4 - LinearReg 14 - 0.11
HMA 6 - LinearReg 14 - 0.10
JMA 10 - LinearReg 14 - 0.11
JMA 6 - LinearReg 14 - 0.11
KAMA 12 - LinearReg 14 - 0.10
KAMA 14 - LinearReg 14 - 0.10
LinearReg 10 - LinearReg 14 - 0.11
LinearReg 6 - LinearReg 14 - 0.09
LinearReg 8 - LinearReg 14 - 0.11
SMA 12 - LinearReg 14 - 0.10
SMA 2 - LinearReg 14 - 0.09
SMA 6 - LinearReg 14 - 0.09
SMA 8 - LinearReg 14 - 0.08
SMMA 14 - LinearReg 14 - 0.04
SMMA 4 - LinearReg 14 - 0.10
SMMA 6 - LinearReg 14 - 0.03
EMA 10 - LinearReg 16 - 0.09
EMA 12 - LinearReg 16 - 0.09
Highest 10 - LinearReg 16 - 0.11
Highest 12 - LinearReg 16 - 0.11
KAMA 2 - LinearReg 16 - 0.11
KAMA 6 - LinearReg 16 - 0.11
LinearReg 14 - LinearReg 16 - 0.11
SMA 16 - LinearReg 16 - 0.10
SMMA 10 - LinearReg 16 - 0.09
SMMA 8 - LinearReg 16 - 0.09
EMA 10 - LinearReg 18 - 0.11
EMA 8 - LinearReg 18 - 0.11
Highest 12 - LinearReg 18 - 0.11
Highest 14 - LinearReg 18 - 0.11
Highest 18 - LinearReg 18 - 0.11
HMA 16 - LinearReg 18 - 0.11
KAMA 4 - LinearReg 18 - 0.11
KAMA 6 - LinearReg 18 - 0.11
SMA 18 - LinearReg 18 - 0.09
SMMA 10 - LinearReg 18 - 0.11
SMMA 12 - LinearReg 18 - 0.11
SMMA 18 - LinearReg 18 - 0.11
SMMA 8 - LinearReg 18 - 0.11
Lowest 2 - LinearReg 2 - 0.11
EMA 18 - LinearReg 20 - 0.11
Highest 20 - LinearReg 20 - 0.11
HMA 18 - LinearReg 20 - 0.11
KAMA 20 - LinearReg 20 - 0.11
SMA 12 - LinearReg 20 - 0.09
SMMA 14 - LinearReg 20 - 0.10
SMMA 20 - LinearReg 20 - 0.11
SMMA 6 - LinearReg 20 - 0.06
EMA 18 - LinearReg 22 - 0.11
Highest 10 - LinearReg 22 - 0.11
Highest 16 - LinearReg 22 - 0.11
Highest 20 - LinearReg 22 - 0.11
Highest 22 - LinearReg 22 - 0.11
KAMA 20 - LinearReg 22 - 0.11
KAMA 22 - LinearReg 22 - 0.11
SMA 14 - LinearReg 22 - 0.09
SMA 16 - LinearReg 22 - 0.11
SMA 8 - LinearReg 22 - 0.11
EMA 12 - LinearReg 24 - 0.10
EMA 24 - LinearReg 24 - 0.10
EMA 8 - LinearReg 24 - 0.11
Highest 12 - LinearReg 24 - 0.11
Highest 14 - LinearReg 24 - 0.11
Highest 24 - LinearReg 24 - 0.11
KAMA 24 - LinearReg 24 - 0.11
SMMA 10 - LinearReg 24 - 0.10
SMMA 12 - LinearReg 24 - 0.10
SMMA 18 - LinearReg 24 - 0.11
SMMA 24 - LinearReg 24 - 0.11
SMMA 8 - LinearReg 24 - 0.10
EMA 18 - LinearReg 26 - 0.10
EMA 20 - LinearReg 26 - 0.09
EMA 22 - LinearReg 26 - 0.09
Highest 6 - LinearReg 26 - 0.11
Highest 8 - LinearReg 26 - 0.11
HMA 26 - LinearReg 26 - 0.09
JMA 16 - LinearReg 26 - 0.11
JMA 18 - LinearReg 26 - 0.11
JMA 22 - LinearReg 26 - 0.11
JMA 24 - LinearReg 26 - 0.11
KAMA 10 - LinearReg 26 - 0.11
KAMA 18 - LinearReg 26 - 0.11
SMA 10 - LinearReg 26 - 0.11
SMA 20 - LinearReg 26 - 0.10
SMMA 16 - LinearReg 26 - 0.11
SMMA 20 - LinearReg 26 - 0.11
EMA 18 - LinearReg 28 - 0.11
EMA 20 - LinearReg 28 - 0.11
Highest 4 - LinearReg 28 - 0.11
Highest 8 - LinearReg 28 - 0.11
HMA 28 - LinearReg 28 - 0.11
JMA 20 - LinearReg 28 - 0.11
JMA 22 - LinearReg 28 - 0.11
JMA 24 - LinearReg 28 - 0.11
KAMA 10 - LinearReg 28 - 0.11
KAMA 12 - LinearReg 28 - 0.11
KAMA 26 - LinearReg 28 - 0.11
SMA 10 - LinearReg 28 - 0.11
SMA 12 - LinearReg 28 - 0.10
SMA 20 - LinearReg 28 - 0.11
SMA 22 - LinearReg 28 - 0.11
SMMA 16 - LinearReg 28 - 0.11
EMA 18 - LinearReg 30 - 0.11
Highest 20 - LinearReg 30 - 0.11
Highest 28 - LinearReg 30 - 0.11
JMA 30 - LinearReg 30 - 0.11
KAMA 20 - LinearReg 30 - 0.11
KAMA 28 - LinearReg 30 - 0.11
SMA 12 - LinearReg 30 - 0.11
SMA 20 - LinearReg 30 - 0.11
SMA 22 - LinearReg 30 - 0.11
SMMA 14 - LinearReg 30 - 0.11
EMA 18 - LinearReg 32 - 0.11
Highest 10 - LinearReg 32 - 0.11
Highest 16 - LinearReg 32 - 0.11
Highest 20 - LinearReg 32 - 0.11
Highest 26 - LinearReg 32 - 0.11
JMA 30 - LinearReg 32 - 0.11
KAMA 14 - LinearReg 32 - 0.11
KAMA 20 - LinearReg 32 - 0.11
KAMA 28 - LinearReg 32 - 0.11
SMA 14 - LinearReg 32 - 0.11
SMA 16 - LinearReg 32 - 0.11
SMA 24 - LinearReg 32 - 0.11
SMA 32 - LinearReg 32 - 0.11
SMMA 6 - LinearReg 32 - 0.09
EMA 14 - LinearReg 34 - 0.10
EMA 16 - LinearReg 34 - 0.11
Highest 12 - LinearReg 34 - 0.11
Highest 14 - LinearReg 34 - 0.11
Highest 18 - LinearReg 34 - 0.11
Highest 22 - LinearReg 34 - 0.11
Highest 24 - LinearReg 34 - 0.11
SMA 18 - LinearReg 34 - 0.11
SMA 28 - LinearReg 34 - 0.11
SMA 34 - LinearReg 34 - 0.11
SMMA 10 - LinearReg 34 - 0.11
SMMA 8 - LinearReg 34 - 0.11
Highest 30 - LinearReg 36 - 0.11
Highest 32 - LinearReg 36 - 0.11
JMA 28 - LinearReg 36 - 0.11
JMA 36 - LinearReg 36 - 0.07
KAMA 16 - LinearReg 36 - 0.10
SMA 20 - LinearReg 36 - 0.11
SMA 20 - LinearReg 38 - 0.11
Highest 2 - LinearReg 4 - 0.11
SMMA 2 - LinearReg 4 - 0.02
EMA 4 - LinearReg 6 - 0.11
HMA 6 - LinearReg 6 - 0.11
JMA 6 - LinearReg 6 - 0.11
SMA 2 - LinearReg 6 - 0.00
SMA 6 - LinearReg 6 - 0.03
SMMA 4 - LinearReg 6 - 0.03
SMMA 6 - LinearReg 6 - 0.03
HMA 6 - LinearReg 8 - 0.11
JMA 6 - LinearReg 8 - 0.11
SMA 2 - LinearReg 8 - 0.11
SMA 6 - LinearReg 8 - 0.11
SMMA 6 - LinearReg 8 - 0.11
HMA 2 - Lowest 10 - 0.11
SMMA 10 - Lowest 10 - 0.11
SMMA 2 - Lowest 10 - 0.08
LinearReg 10 - Lowest 12 - 0.11
HMA 6 - Lowest 14 - 0.04
LinearReg 10 - Lowest 14 - 0.11
LinearReg 6 - Lowest 14 - 0.11
SMMA 14 - Lowest 14 - 0.11
HMA 12 - Lowest 16 - 0.06
HMA 2 - Lowest 16 - 0.11
LinearReg 14 - Lowest 16 - 0.11
SMMA 10 - Lowest 16 - 0.11
SMMA 12 - Lowest 16 - 0.11
SMMA 2 - Lowest 16 - 0.00
HMA 10 - Lowest 18 - 0.06
HMA 12 - Lowest 18 - 0.06
HMA 2 - Lowest 18 - 0.11
LinearReg 14 - Lowest 18 - 0.11
SMMA 12 - Lowest 18 - 0.05
SMMA 18 - Lowest 18 - 0.11
SMMA 2 - Lowest 18 - 0.00
KAMA 2 - Lowest 2 - 0.11
SMMA 2 - Lowest 2 - 0.11
EMA 2 - Lowest 20 - 0.00
LinearReg 10 - Lowest 20 - 0.11
LinearReg 12 - Lowest 20 - 0.11
LinearReg 16 - Lowest 20 - 0.11
SMMA 20 - Lowest 20 - 0.11
EMA 2 - Lowest 22 - 0.00
HMA 10 - Lowest 22 - 0.05
LinearReg 12 - Lowest 22 - 0.11
LinearReg 18 - Lowest 22 - 0.11
EMA 2 - Lowest 24 - 0.00
LinearReg 12 - Lowest 24 - 0.11
LinearReg 18 - Lowest 24 - 0.11
LinearReg 20 - Lowest 24 - 0.11
SMMA 24 - Lowest 24 - 0.11
LinearReg 10 - Lowest 26 - 0.11
LinearReg 16 - Lowest 26 - 0.11
LinearReg 24 - Lowest 26 - 0.11
LinearReg 4 - Lowest 26 - 0.11
LinearReg 8 - Lowest 26 - 0.11
HMA 6 - Lowest 28 - 0.04
LinearReg 10 - Lowest 28 - 0.11
LinearReg 16 - Lowest 28 - 0.11
LinearReg 24 - Lowest 28 - 0.11
LinearReg 6 - Lowest 28 - 0.11
LinearReg 8 - Lowest 28 - 0.11
LinearReg 14 - Lowest 30 - 0.11
LinearReg 22 - Lowest 30 - 0.11
LinearReg 28 - Lowest 30 - 0.11
LinearReg 8 - Lowest 32 - 0.11
LinearReg 16 - Lowest 34 - 0.11
LinearReg 24 - Lowest 34 - 0.11
LinearReg 30 - Lowest 34 - 0.11
LinearReg 8 - Lowest 34 - 0.11
LinearReg 14 - Lowest 36 - 0.11
LinearReg 22 - Lowest 36 - 0.11
LinearReg 28 - Lowest 36 - 0.11
LinearReg 34 - Lowest 36 - 0.11
LinearReg 14 - Lowest 38 - 0.11
LinearReg 20 - Lowest 38 - 0.11
LinearReg 22 - Lowest 38 - 0.11
LinearReg 26 - Lowest 38 - 0.11
LinearReg 28 - Lowest 38 - 0.11
LinearReg 34 - Lowest 38 - 0.11
SMMA 2 - Lowest 38 - 0.00
KAMA 2 - Lowest 4 - 0.11
LinearReg 12 - Lowest 40 - 0.10
LinearReg 18 - Lowest 40 - 0.11
LinearReg 26 - Lowest 40 - 0.11
LinearReg 30 - Lowest 40 - 0.11
LinearReg 32 - Lowest 40 - 0.11
LinearReg 38 - Lowest 40 - 0.11
LinearReg 20 - Lowest 42 - 0.11
LinearReg 26 - Lowest 42 - 0.11
LinearReg 32 - Lowest 42 - 0.11
LinearReg 38 - Lowest 42 - 0.11
LinearReg 20 - Lowest 44 - 0.11
LinearReg 22 - Lowest 44 - 0.11
LinearReg 26 - Lowest 44 - 0.11
LinearReg 38 - Lowest 44 - 0.11
LinearReg 40 - Lowest 44 - 0.11
LinearReg 30 - Lowest 46 - 0.11
LinearReg 36 - Lowest 46 - 0.11
LinearReg 44 - Lowest 46 - 0.11
SMMA 2 - Lowest 6 - 0.07
EMA 2 - Lowest 8 - 0.07
EMA 8 - Lowest 8 - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.11
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - MeanDeviatio - 0.10
MeanDeviatio - Momentum 10 - 0.03
Momentum 4 - Momentum 10 - 0.11
Momentum 8 - Momentum 10 - 0.11
MeanDeviatio - Momentum 12 - 0.03
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 16 - 0.09
Momentum 12 - Momentum 16 - 0.10
Momentum 6 - Momentum 16 - 0.11
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 18 - 0.05
Momentum 4 - Momentum 18 - 0.11
MeanDeviatio - Momentum 2 - 0.06
Momentum 10 - Momentum 20 - 0.11
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.11
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 24 - 0.11
Momentum 2 - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
Momentum 6 - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.10
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
Momentum 2 - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.10
MeanDeviatio - Momentum 32 - 0.10
Momentum 2 - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
Momentum 10 - Momentum 34 - 0.11
Momentum 4 - Momentum 34 - 0.11
Momentum 8 - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
Momentum 2 - Momentum 38 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
Momentum 6 - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
Momentum 4 - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 6 - 0.02
Momentum 2 - Momentum 6 - 0.10
MeanDeviatio - Momentum 8 - 0.02
HMA 10 - SMA 12 - 0.11
HMA 6 - SMA 12 - 0.11
HMA 12 - SMA 14 - 0.03
SMMA 10 - SMA 14 - 0.11
HMA 10 - SMA 16 - 0.03
HMA 12 - SMA 16 - 0.03
HMA 12 - SMA 18 - 0.03
KAMA 2 - SMA 2 - 0.11
HMA 14 - SMA 22 - 0.03
HMA 10 - SMA 24 - 0.03
HMA 12 - SMA 24 - 0.03
HMA 10 - SMA 26 - 0.03
HMA 10 - SMA 28 - 0.11
HMA 12 - SMA 28 - 0.03
HMA 10 - SMA 30 - 0.11
HMA 10 - SMA 32 - 0.11
HMA 12 - SMA 32 - 0.02
KAMA 2 - SMA 4 - 0.11
HMA 14 - SMA 42 - 0.03
HMA 14 - SMA 46 - 0.03
KAMA 6 - SMA 8 - 0.11
HMA 6 - SMMA 16 - 0.03
HMA 12 - SMMA 18 - 0.02
KAMA 2 - SMMA 2 - 0.11
SMA 2 - SMMA 2 - 0.11
HMA 10 - SMMA 22 - 0.03
HMA 12 - SMMA 26 - 0.02";
    #endregion

        #region
        public static readonly string EnabledPairs_AvgIndicator111 = @"SMMA 8 - EMA 12 - 0.09
SMMA 12 - EMA 14 - 0.11
SMMA 14 - EMA 16 - 0.11
SMMA 2 - EMA 2 - 0.03
SMA 22 - EMA 24 - 0.00
SMA 28 - EMA 28 - 0.10
SMMA 10 - EMA 12 - 0.11
SMMA 12 - EMA 12 - 0.11
SMMA 14 - EMA 14 - 0.11
SMMA 12 - EMA 16 - 0.11
SMMA 10 - EMA 18 - 0.11
SMA 22 - EMA 22 - 0.00
SMMA 4 - EMA 6 - 0.02
SMMA 10 - EMA 10 - 0.09
SMMA 10 - EMA 14 - 0.11
SMMA 8 - EMA 14 - 0.10
SMA 12 - EMA 16 - 0.00
SMMA 12 - EMA 18 - 0.11
SMA 20 - EMA 20 - 0.00
SMA 26 - EMA 26 - 0.07
SMMA 10 - EMA 16 - 0.11
SMMA 16 - HMA 16 - 0.09
EMA 18 - HMA 18 - 0.09
SMMA 14 - HMA 18 - 0.08
KAMA 2 - HMA 2 - 0.05
LinearReg 2 - HMA 2 - 0.11
Lowest 2 - HMA 2 - 0.11
SMA 2 - HMA 2 - 0.11
EMA 20 - HMA 20 - 0.09
SMMA 16 - HMA 20 - 0.10
SMMA 20 - HMA 20 - 0.11
SMMA 16 - HMA 22 - 0.10
SMMA 20 - HMA 22 - 0.11
SMMA 14 - HMA 24 - 0.11
SMMA 18 - HMA 24 - 0.11
SMA 16 - HMA 28 - 0.04
SMA 24 - HMA 28 - 0.09
SMA 26 - HMA 28 - 0.10
SMA 28 - HMA 28 - 0.10
HMA 8 - JMA 10 - 0.11
LinearReg 4 - JMA 10 - 0.11
LinearReg 8 - JMA 10 - 0.11
SMA 10 - JMA 10 - 0.01
HMA 8 - JMA 12 - 0.11
LinearReg 10 - JMA 12 - 0.11
LinearReg 8 - JMA 12 - 0.10
Lowest 2 - JMA 12 - 0.10
Lowest 8 - JMA 12 - 0.00
SMA 10 - JMA 12 - 0.06
SMA 12 - JMA 12 - 0.09
EMA 10 - JMA 14 - 0.06
EMA 12 - JMA 14 - 0.03
HMA 12 - JMA 14 - 0.09
LinearReg 14 - JMA 14 - 0.11
Lowest 6 - JMA 14 - 0.02
SMMA 10 - JMA 14 - 0.05
SMMA 2 - JMA 14 - 0.08
SMMA 8 - JMA 14 - 0.10
EMA 14 - JMA 16 - 0.07
EMA 16 - JMA 16 - 0.11
EMA 14 - HMA 18 - 0.02
EMA 16 - HMA 18 - 0.03
SMA 16 - HMA 18 - 0.10
SMMA 18 - HMA 18 - 0.05
SMMA 2 - HMA 2 - 0.06
SMMA 14 - HMA 20 - 0.09
SMA 12 - HMA 22 - 0.09
SMA 6 - JMA 16 - 0.08
SMA 8 - JMA 16 - 0.08
SMMA 14 - JMA 16 - 0.10
SMMA 6 - JMA 16 - 0.07
EMA 10 - JMA 18 - 0.08
EMA 6 - JMA 18 - 0.11
EMA 8 - JMA 18 - 0.11
HMA 16 - JMA 18 - 0.09
LinearReg 18 - JMA 18 - 0.05
SMA 4 - JMA 18 - 0.11
SMMA 12 - JMA 18 - 0.11
SMMA 16 - JMA 18 - 0.11
SMMA 8 - JMA 18 - 0.10
EMA 2 - JMA 2 - 0.04
SMMA 2 - JMA 2 - 0.06
EMA 10 - JMA 20 - 0.09
EMA 12 - JMA 20 - 0.09
EMA 6 - JMA 20 - 0.11
SMMA 12 - JMA 20 - 0.10
EMA 10 - JMA 22 - 0.11
EMA 20 - JMA 22 - 0.10
EMA 8 - JMA 22 - 0.11
HMA 16 - JMA 22 - 0.11
LinearReg 18 - JMA 22 - 0.06
SMMA 12 - JMA 22 - 0.10
EMA 14 - JMA 24 - 0.10
EMA 16 - JMA 24 - 0.11
SMMA 14 - HMA 16 - 0.09
SMA 18 - HMA 18 - 0.09
SMMA 10 - HMA 18 - 0.09
SMMA 12 - HMA 18 - 0.09
SMMA 8 - HMA 18 - 0.02
EMA 2 - HMA 2 - 0.09
Highest 2 - HMA 2 - 0.11
SMA 14 - HMA 20 - 0.11
SMA 16 - HMA 20 - 0.10
SMMA 10 - HMA 20 - 0.09
SMA 14 - HMA 22 - 0.09
SMMA 18 - HMA 22 - 0.10
SMMA 16 - HMA 24 - 0.11
SMMA 16 - HMA 18 - 0.09
JMA 2 - HMA 2 - 0.11
SMMA 12 - HMA 20 - 0.08
SMMA 18 - HMA 20 - 0.10
EMA 22 - HMA 22 - 0.08
SMMA 12 - HMA 22 - 0.09
SMMA 22 - HMA 22 - 0.10
SMA 12 - HMA 24 - 0.09
SMMA 14 - HMA 22 - 0.09
SMMA 2 - HMA 4 - 0.03
EMA 6 - HMA 6 - 0.03
SMA 4 - HMA 6 - 0.03
SMA 6 - HMA 8 - 0.03
EMA 6 - JMA 10 - 0.02
EMA 8 - JMA 10 - 0.06
Lowest 2 - JMA 10 - 0.10
SMA 4 - JMA 10 - 0.04
Lowest 2 - JMA 24 - 0.11
SMA 14 - JMA 24 - 0.06
SMA 22 - JMA 24 - 0.09
SMA 8 - JMA 24 - 0.11
SMMA 14 - JMA 24 - 0.11
SMMA 22 - JMA 24 - 0.11
SMMA 6 - JMA 24 - 0.09
LinearReg 24 - JMA 26 - 0.05
Lowest 4 - JMA 26 - 0.08
SMA 10 - JMA 26 - 0.10
SMA 20 - JMA 26 - 0.08
SMMA 16 - JMA 26 - 0.11
SMMA 20 - JMA 26 - 0.11
EMA 12 - JMA 28 - 0.09
EMA 24 - JMA 28 - 0.11
EMA 26 - JMA 28 - 0.11
SMA 14 - JMA 28 - 0.09
SMA 16 - JMA 28 - 0.09
SMA 18 - JMA 28 - 0.09
SMA 24 - JMA 28 - 0.09
SMA 18 - HMA 30 - 0.09
SMA 26 - HMA 30 - 0.10
EMA 4 - HMA 6 - 0.00
SMA 6 - HMA 6 - 0.03
SMMA 4 - HMA 6 - 0.03
SMMA 6 - HMA 6 - 0.03
EMA 10 - JMA 10 - 0.08
EMA 4 - JMA 10 - 0.02
LinearReg 6 - JMA 10 - 0.11
SMA 2 - JMA 10 - 0.10
SMA 6 - JMA 10 - 0.00
SMA 8 - JMA 10 - 0.09
SMMA 4 - JMA 10 - 0.04
SMMA 6 - JMA 10 - 0.08
LinearReg 6 - JMA 12 - 0.11
SMA 6 - JMA 12 - 0.03
SMA 8 - JMA 12 - 0.07
SMMA 12 - JMA 12 - 0.07
SMMA 6 - JMA 12 - 0.06
EMA 14 - JMA 14 - 0.11
EMA 6 - JMA 12 - 0.02
HMA 10 - JMA 12 - 0.11
Lowest 6 - JMA 12 - 0.00
SMA 4 - JMA 12 - 0.05
EMA 4 - JMA 14 - 0.08
LinearReg 6 - JMA 14 - 0.10
SMA 6 - JMA 14 - 0.06
SMA 8 - JMA 14 - 0.06
SMMA 12 - JMA 14 - 0.07
SMMA 4 - JMA 14 - 0.06
SMMA 6 - JMA 14 - 0.06
EMA 12 - JMA 16 - 0.06
HMA 14 - JMA 16 - 0.11
LinearReg 6 - JMA 16 - 0.11
LinearReg 8 - JMA 16 - 0.11
SMA 14 - JMA 16 - 0.07
SMA 16 - JMA 16 - 0.06
SMMA 10 - JMA 16 - 0.08
HMA 12 - JMA 18 - 0.11
HMA 8 - JMA 18 - 0.11
Lowest 2 - JMA 18 - 0.11
Lowest 4 - JMA 18 - 0.08
SMA 10 - JMA 18 - 0.06
SMMA 2 - JMA 18 - 0.10
EMA 20 - JMA 20 - 0.07
SMA 10 - JMA 20 - 0.06
SMA 12 - JMA 20 - 0.08
SMA 20 - JMA 20 - 0.06
SMMA 16 - JMA 20 - 0.11
SMA 26 - JMA 28 - 0.11
SMA 28 - JMA 28 - 0.11
SMMA 18 - JMA 28 - 0.11
EMA 24 - JMA 30 - 0.11
EMA 26 - JMA 30 - 0.11
EMA 30 - JMA 30 - 0.11
SMMA 24 - JMA 30 - 0.11
EMA 30 - JMA 32 - 0.11
SMMA 8 - JMA 32 - 0.10
SMA 22 - JMA 34 - 0.11
SMA 32 - JMA 34 - 0.10
EMA 6 - JMA 14 - 0.05
EMA 8 - JMA 14 - 0.06
HMA 10 - JMA 14 - 0.11
LinearReg 12 - JMA 14 - 0.10
Lowest 2 - JMA 14 - 0.11
Lowest 4 - JMA 14 - 0.05
Lowest 8 - JMA 14 - 0.00
SMA 14 - JMA 14 - 0.08
SMA 4 - JMA 14 - 0.11
SMMA 14 - JMA 14 - 0.07
HMA 12 - JMA 16 - 0.11
Lowest 2 - JMA 16 - 0.11
SMA 10 - JMA 16 - 0.07
SMMA 2 - JMA 16 - 0.10
EMA 12 - JMA 18 - 0.10
HMA 14 - JMA 18 - 0.11
LinearReg 16 - JMA 18 - 0.07
SMA 14 - JMA 18 - 0.06
SMA 16 - JMA 18 - 0.07
SMA 18 - JMA 18 - 0.07
SMMA 10 - JMA 18 - 0.11
SMMA 18 - JMA 18 - 0.11
SMA 2 - JMA 2 - 0.11
EMA 14 - JMA 20 - 0.10
EMA 16 - JMA 20 - 0.07
HMA 14 - JMA 20 - 0.10
LinearReg 18 - JMA 20 - 0.09
SMA 16 - JMA 20 - 0.08
SMA 18 - JMA 20 - 0.06
SMMA 10 - JMA 20 - 0.10
SMMA 18 - JMA 20 - 0.11
SMMA 20 - JMA 20 - 0.11
SMMA 8 - JMA 20 - 0.10
EMA 18 - JMA 22 - 0.10
LinearReg 20 - JMA 22 - 0.05
LinearReg 22 - JMA 22 - 0.03
Lowest 2 - JMA 22 - 0.11
Lowest 4 - JMA 22 - 0.10
SMA 10 - JMA 22 - 0.10
SMA 12 - JMA 22 - 0.11
SMA 20 - JMA 22 - 0.06
SMMA 16 - JMA 22 - 0.11
SMMA 8 - JMA 22 - 0.10
EMA 12 - JMA 24 - 0.08
LinearReg 16 - JMA 24 - 0.05
SMA 16 - JMA 24 - 0.08
SMA 18 - JMA 24 - 0.09
SMA 24 - JMA 24 - 0.08
SMMA 10 - JMA 24 - 0.11
SMMA 18 - JMA 24 - 0.11
SMMA 20 - JMA 24 - 0.11
EMA 14 - JMA 26 - 0.09
EMA 16 - JMA 26 - 0.09
EMA 18 - JMA 26 - 0.09
SMMA 22 - JMA 34 - 0.11
SMA 20 - JMA 36 - 0.11
SMA 30 - JMA 36 - 0.10
SMA 32 - JMA 36 - 0.11
SMA 16 - JMA 38 - 0.11
SMA 18 - JMA 38 - 0.11
SMA 24 - JMA 38 - 0.10
SMA 26 - JMA 38 - 0.09
SMA 28 - JMA 38 - 0.10
SMA 2 - JMA 4 - 0.06
EMA 12 - JMA 22 - 0.07
EMA 22 - JMA 22 - 0.11
LinearReg 16 - JMA 22 - 0.11
SMA 16 - JMA 22 - 0.06
SMA 18 - JMA 22 - 0.07
SMMA 10 - JMA 22 - 0.10
SMMA 18 - JMA 22 - 0.11
SMMA 20 - JMA 22 - 0.11
EMA 18 - JMA 24 - 0.10
EMA 20 - JMA 24 - 0.08
LinearReg 22 - JMA 24 - 0.06
SMA 10 - JMA 24 - 0.10
SMA 12 - JMA 24 - 0.06
SMA 20 - JMA 24 - 0.10
SMMA 16 - JMA 24 - 0.11
SMMA 8 - JMA 24 - 0.10
EMA 10 - JMA 26 - 0.09
EMA 20 - JMA 26 - 0.11
EMA 22 - JMA 26 - 0.11
SMMA 10 - JMA 26 - 0.10
SMMA 12 - JMA 26 - 0.11
SMMA 22 - JMA 26 - 0.11
SMMA 6 - JMA 26 - 0.09
EMA 14 - JMA 28 - 0.08
EMA 16 - JMA 28 - 0.11
EMA 18 - JMA 28 - 0.10
EMA 28 - JMA 28 - 0.10
LinearReg 26 - JMA 26 - 0.03
Lowest 2 - JMA 26 - 0.09
SMA 12 - JMA 26 - 0.08
SMA 22 - JMA 26 - 0.11
SMMA 14 - JMA 26 - 0.11
SMMA 24 - JMA 26 - 0.11
SMMA 8 - JMA 26 - 0.11
EMA 20 - JMA 28 - 0.09
EMA 22 - JMA 28 - 0.11
SMMA 10 - JMA 28 - 0.09
SMMA 12 - JMA 28 - 0.11
SMMA 22 - JMA 28 - 0.11
SMMA 6 - JMA 28 - 0.09
SMA 30 - JMA 30 - 0.11
SMMA 16 - JMA 30 - 0.11
SMA 26 - JMA 40 - 0.10
SMA 28 - JMA 40 - 0.10
SMA 26 - JMA 42 - 0.09
SMA 28 - JMA 42 - 0.10
Lowest 2 - JMA 28 - 0.10
SMA 12 - JMA 28 - 0.09
SMA 22 - JMA 28 - 0.08
SMMA 14 - JMA 28 - 0.11
SMMA 24 - JMA 28 - 0.11
SMMA 8 - JMA 28 - 0.08
EMA 28 - JMA 30 - 0.11
Lowest 6 - JMA 30 - 0.06
SMA 24 - JMA 30 - 0.11
SMA 26 - JMA 30 - 0.11
SMA 28 - JMA 30 - 0.11
SMMA 18 - JMA 30 - 0.11
SMMA 20 - JMA 30 - 0.11
SMA 20 - JMA 32 - 0.08
SMA 30 - JMA 32 - 0.11
SMMA 16 - JMA 32 - 0.11
SMA 16 - JMA 34 - 0.09
SMA 18 - JMA 34 - 0.10
SMA 24 - JMA 34 - 0.11
SMA 26 - JMA 34 - 0.11
SMA 28 - JMA 34 - 0.10
SMA 34 - JMA 34 - 0.11
SMMA 20 - JMA 34 - 0.11
SMA 2 - HMA 4 - 0.01
SMMA 10 - JMA 10 - 0.07
SMMA 2 - JMA 10 - 0.00
SMMA 8 - JMA 10 - 0.07
EMA 10 - JMA 12 - 0.06
EMA 12 - JMA 12 - 0.11
SMMA 10 - JMA 12 - 0.05
SMMA 2 - JMA 12 - 0.04
SMMA 8 - JMA 12 - 0.06
HMA 14 - JMA 14 - 0.11
HMA 8 - JMA 14 - 0.11
LinearReg 8 - JMA 14 - 0.11
SMA 10 - JMA 14 - 0.06
SMA 12 - JMA 14 - 0.07
EMA 10 - JMA 16 - 0.06
SMA 12 - JMA 44 - 0.10
SMA 30 - JMA 44 - 0.11
SMMA 2 - JMA 6 - 0.02
SMMA 2 - JMA 8 - 0.02
SMA 18 - JMA 32 - 0.10
SMA 24 - JMA 32 - 0.11
SMA 26 - JMA 32 - 0.11
SMA 28 - JMA 32 - 0.11
SMMA 18 - JMA 32 - 0.11
SMMA 20 - JMA 32 - 0.11
SMA 20 - JMA 34 - 0.11
SMA 30 - JMA 34 - 0.10
SMA 22 - JMA 36 - 0.11
LinearReg 4 - JMA 4 - 0.11
EMA 6 - JMA 16 - 0.06
EMA 8 - JMA 16 - 0.08
HMA 10 - JMA 16 - 0.11
SMMA 12 - JMA 16 - 0.08
SMMA 16 - JMA 16 - 0.10
SMMA 8 - JMA 16 - 0.10
EMA 14 - JMA 18 - 0.07
EMA 16 - JMA 18 - 0.08
EMA 18 - JMA 18 - 0.11
EMA 4 - JMA 18 - 0.11
HMA 18 - JMA 18 - 0.09
LinearReg 6 - JMA 18 - 0.11
LinearReg 8 - JMA 18 - 0.11
Lowest 6 - JMA 18 - 0.01
Lowest 8 - JMA 18 - 0.00
SMA 12 - JMA 18 - 0.06
SMA 6 - JMA 18 - 0.11
SMA 8 - JMA 18 - 0.10
SMMA 14 - JMA 18 - 0.09
SMMA 4 - JMA 18 - 0.11
SMMA 6 - JMA 18 - 0.07
Lowest 2 - JMA 2 - 0.06
EMA 18 - JMA 20 - 0.07
HMA 18 - JMA 20 - 0.11
Lowest 2 - JMA 20 - 0.11
SMA 14 - JMA 20 - 0.05
SMA 6 - JMA 20 - 0.11
SMMA 14 - JMA 20 - 0.10
SMMA 6 - JMA 20 - 0.11
EMA 14 - JMA 22 - 0.08
EMA 16 - JMA 22 - 0.10
SMA 14 - JMA 22 - 0.08
SMA 6 - LinearReg 12 - 0.02
EMA 12 - LinearReg 14 - 0.10
SMMA 12 - LinearReg 14 - 0.09
SMMA 8 - LinearReg 14 - 0.09
SMMA 16 - LinearReg 16 - 0.09
HMA 18 - LinearReg 18 - 0.10
SMA 12 - LinearReg 18 - 0.09
JMA 2 - LinearReg 2 - 0.11
SMMA 2 - LinearReg 2 - 0.06
HMA 20 - LinearReg 20 - 0.11
SMMA 10 - LinearReg 20 - 0.11
SMMA 12 - LinearReg 20 - 0.10
HMA 22 - LinearReg 22 - 0.11
SMA 12 - LinearReg 24 - 0.10
SMA 22 - LinearReg 24 - 0.10
SMMA 10 - LinearReg 26 - 0.11
SMA 22 - JMA 38 - 0.11
SMA 30 - JMA 38 - 0.10
SMA 32 - JMA 38 - 0.11
Lowest 2 - JMA 4 - 0.10
SMMA 2 - JMA 4 - 0.01
SMA 24 - JMA 40 - 0.11
SMA 18 - LinearReg 30 - 0.11
SMA 26 - LinearReg 30 - 0.11
SMA 24 - JMA 42 - 0.11
EMA 6 - JMA 6 - 0.00
SMA 4 - JMA 6 - 0.03
SMA 2 - JMA 8 - 0.10
SMA 6 - JMA 8 - 0.00
EMA 10 - LinearReg 14 - 0.11
EMA 14 - LinearReg 16 - 0.09
EMA 16 - LinearReg 16 - 0.09
SMA 14 - LinearReg 16 - 0.10
SMMA 14 - LinearReg 16 - 0.09
JMA 32 - LinearReg 34 - 0.07
JMA 34 - LinearReg 34 - 0.07
SMA 14 - LinearReg 36 - 0.11
SMA 16 - LinearReg 38 - 0.11
EMA 4 - LinearReg 4 - 0.03
EMA 6 - LinearReg 6 - 0.03
SMA 4 - LinearReg 6 - 0.03
MeanDeviatio - Momentum 10 - 0.03
MeanDeviatio - Momentum 10 - 0.01
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
SMA 2 - LinearReg 2 - 0.11
EMA 20 - LinearReg 20 - 0.11
SMMA 16 - LinearReg 20 - 0.10
SMA 12 - LinearReg 22 - 0.04
SMA 22 - LinearReg 22 - 0.09
EMA 14 - LinearReg 24 - 0.10
EMA 16 - LinearReg 24 - 0.11
SMA 2 - JMA 6 - 0.11
SMA 6 - JMA 6 - 0.03
SMMA 4 - JMA 6 - 0.02
EMA 6 - JMA 8 - 0.04
EMA 14 - LinearReg 14 - 0.09
SMMA 10 - LinearReg 14 - 0.09
SMMA 12 - LinearReg 16 - 0.09
SMMA 6 - LinearReg 16 - 0.08
MeanDeviatio - Momentum 16 - 0.10
MeanDeviatio - Momentum 18 - 0.09
MeanDeviatio - Momentum 18 - 0.05
Momentum 6 - Momentum 18 - 0.10
MeanDeviatio - Momentum 20 - 0.10
MeanDeviatio - Momentum 20 - 0.09
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 26 - 0.10
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.10
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
Momentum 12 - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
SMA 22 - JMA 22 - 0.08
SMA 6 - JMA 22 - 0.11
SMA 8 - JMA 22 - 0.10
SMMA 14 - JMA 22 - 0.10
SMMA 22 - JMA 22 - 0.11
SMMA 6 - JMA 22 - 0.11
EMA 10 - JMA 24 - 0.08
EMA 22 - JMA 24 - 0.10
EMA 24 - JMA 24 - 0.11
LinearReg 18 - JMA 24 - 0.07
SMMA 12 - JMA 24 - 0.10
SMMA 24 - JMA 24 - 0.11
EMA 12 - JMA 26 - 0.09
EMA 24 - JMA 26 - 0.11
EMA 26 - JMA 26 - 0.11
SMA 14 - JMA 26 - 0.09
SMA 16 - JMA 26 - 0.08
SMA 18 - JMA 26 - 0.08
SMA 24 - JMA 26 - 0.08
SMA 26 - JMA 26 - 0.11
SMMA 18 - JMA 26 - 0.11
SMA 10 - JMA 28 - 0.06
SMA 20 - JMA 28 - 0.11
EMA 24 - LinearReg 26 - 0.11
EMA 26 - LinearReg 26 - 0.11
SMA 8 - LinearReg 26 - 0.11
SMMA 12 - LinearReg 26 - 0.11
SMMA 18 - LinearReg 26 - 0.11
SMMA 22 - LinearReg 26 - 0.11
SMA 8 - LinearReg 28 - 0.11
SMA 8 - LinearReg 30 - 0.09
EMA 2 - LinearReg 2 - 0.06
EMA 12 - LinearReg 20 - 0.09
SMA 16 - LinearReg 20 - 0.09
SMA 18 - LinearReg 20 - 0.09
SMMA 18 - LinearReg 20 - 0.11
SMA 20 - LinearReg 24 - 0.11
EMA 14 - LinearReg 26 - 0.11
EMA 16 - LinearReg 26 - 0.10
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 4 - 0.02
Momentum 2 - Momentum 4 - 0.00
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 6 - 0.03
SMMA 16 - JMA 28 - 0.11
SMMA 20 - JMA 28 - 0.11
Lowest 2 - JMA 30 - 0.11
Lowest 4 - JMA 30 - 0.11
Lowest 8 - JMA 30 - 0.04
SMA 22 - JMA 30 - 0.10
SMMA 14 - JMA 30 - 0.11
SMMA 22 - JMA 30 - 0.11
EMA 16 - JMA 32 - 0.08
SMA 22 - JMA 32 - 0.11
SMA 32 - JMA 32 - 0.10
SMMA 22 - JMA 32 - 0.11
SMA 14 - LinearReg 34 - 0.11
SMA 26 - LinearReg 34 - 0.11
JMA 32 - LinearReg 36 - 0.07
JMA 34 - LinearReg 36 - 0.07
SMMA 4 - LinearReg 4 - 0.03
KAMA 2 - LinearReg 6 - 0.03
SMA 12 - LinearReg 26 - 0.11
SMMA 14 - LinearReg 26 - 0.11
SMMA 8 - LinearReg 26 - 0.10
SMA 16 - JMA 36 - 0.10
SMA 18 - JMA 36 - 0.11
SMA 26 - JMA 36 - 0.09
SMA 28 - JMA 36 - 0.10
SMA 20 - JMA 38 - 0.10
SMA 4 - JMA 4 - 0.03
SMA 14 - LinearReg 30 - 0.11
SMA 16 - LinearReg 30 - 0.10
JMA 32 - LinearReg 32 - 0.07
JMA 36 - LinearReg 38 - 0.08
EMA 2 - LinearReg 4 - 0.01
SMA 22 - JMA 40 - 0.10
SMA 30 - JMA 40 - 0.10
SMA 30 - JMA 42 - 0.10
MeanDeviatio - Momentum 10 - 0.01
MeanDeviatio - Momentum 12 - 0.04
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
Momentum 12 - Momentum 14 - 0.11
MeanDeviatio - Momentum 16 - 0.09
MeanDeviatio - Momentum 18 - 0.07
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 20 - 0.09
MeanDeviatio - Momentum 20 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
SMA 28 - JMA 44 - 0.10
EMA 4 - JMA 6 - 0.00
LinearReg 4 - JMA 6 - 0.11
LinearReg 6 - JMA 6 - 0.11
Lowest 2 - JMA 6 - 0.11
SMMA 6 - JMA 6 - 0.03
EMA 4 - JMA 8 - 0.02
LinearReg 4 - JMA 8 - 0.11
LinearReg 6 - JMA 8 - 0.10
LinearReg 8 - JMA 8 - 0.10
Lowest 2 - JMA 8 - 0.11
Momentum 12 - Momentum 22 - 0.10
Momentum 8 - Momentum 22 - 0.11
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 10 - 0.02
MeanDeviatio - Momentum 12 - 0.04
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 42 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 6 - 0.03
SMMA 12 - SMA 12 - 0.11
MeanDeviatio - Momentum 16 - 0.09
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 18 - 0.09
MeanDeviatio - Momentum 18 - 0.09
MeanDeviatio - Momentum 20 - 0.10
MeanDeviatio - Momentum 20 - 0.05
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.09
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
Momentum 10 - Momentum 28 - 0.11
Momentum 12 - Momentum 28 - 0.10
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 8 - 0.01
SMA 8 - JMA 8 - 0.00
SMMA 6 - JMA 8 - 0.09
SMMA 14 - LinearReg 14 - 0.04
SMMA 6 - LinearReg 14 - 0.03
EMA 10 - LinearReg 16 - 0.09
EMA 12 - LinearReg 16 - 0.09
SMA 16 - LinearReg 16 - 0.10
SMMA 10 - LinearReg 16 - 0.09
SMMA 8 - LinearReg 16 - 0.09
Lowest 2 - LinearReg 2 - 0.11
EMA 18 - LinearReg 20 - 0.11
HMA 18 - LinearReg 20 - 0.11
SMA 12 - LinearReg 20 - 0.09
SMMA 14 - LinearReg 20 - 0.10
SMMA 20 - LinearReg 20 - 0.11
SMA 14 - LinearReg 22 - 0.09
SMA 16 - LinearReg 22 - 0.11
EMA 12 - LinearReg 24 - 0.10
SMMA 8 - LinearReg 24 - 0.10
EMA 18 - LinearReg 26 - 0.10
EMA 20 - LinearReg 26 - 0.09
EMA 22 - LinearReg 26 - 0.09
HMA 26 - LinearReg 26 - 0.09
SMA 20 - LinearReg 26 - 0.10
SMMA 16 - LinearReg 26 - 0.11
SMMA 20 - LinearReg 26 - 0.11
SMA 12 - LinearReg 28 - 0.10
SMA 12 - LinearReg 30 - 0.11
SMA 14 - LinearReg 32 - 0.11
SMA 16 - LinearReg 32 - 0.11
JMA 36 - LinearReg 36 - 0.07
SMMA 2 - LinearReg 4 - 0.02
SMA 2 - LinearReg 6 - 0.00
SMA 6 - LinearReg 6 - 0.03
SMMA 4 - LinearReg 6 - 0.03
SMMA 6 - LinearReg 6 - 0.03
MeanDeviatio - Momentum 10 - 0.03
MeanDeviatio - Momentum 12 - 0.03
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 16 - 0.09
Momentum 12 - Momentum 16 - 0.10
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 2 - 0.06
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.11
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.10
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.10
MeanDeviatio - Momentum 32 - 0.10
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 6 - 0.02
MeanDeviatio - Momentum 8 - 0.02";
        #endregion

        #region
        public static readonly string EnabledPairs_AvgIndicator58 = @"SMMA 8 - EMA 12 - 0.09
SMMA 12 - EMA 14 - 0.11
SMMA 14 - EMA 16 - 0.11
SMMA 2 - EMA 2 - 0.03
SMA 22 - EMA 24 - 0.00
SMA 26 - EMA 28 - 0.01
SMA 28 - EMA 28 - 0.10
SMMA 10 - EMA 12 - 0.11
SMMA 12 - EMA 12 - 0.11
SMMA 14 - EMA 14 - 0.11
SMMA 12 - EMA 16 - 0.11
SMMA 10 - EMA 18 - 0.11
SMA 22 - EMA 22 - 0.00
SMMA 4 - EMA 6 - 0.02
SMMA 10 - EMA 10 - 0.09
SMMA 10 - EMA 14 - 0.11
SMMA 8 - EMA 14 - 0.10
SMA 12 - EMA 16 - 0.00
SMMA 12 - EMA 18 - 0.11
SMA 20 - EMA 20 - 0.00
SMA 24 - EMA 26 - 0.00
SMA 26 - EMA 26 - 0.07
SMMA 10 - EMA 16 - 0.11
SMA 24 - EMA 24 - 0.11
SMMA 16 - HMA 16 - 0.09
EMA 18 - HMA 18 - 0.09
SMMA 14 - HMA 18 - 0.08
KAMA 2 - HMA 2 - 0.05
LinearReg 2 - HMA 2 - 0.11
Lowest 2 - HMA 2 - 0.11
SMA 2 - HMA 2 - 0.11
EMA 20 - HMA 20 - 0.09
SMMA 16 - HMA 20 - 0.10
SMMA 20 - HMA 20 - 0.11
SMMA 16 - HMA 22 - 0.10
SMMA 20 - HMA 22 - 0.11
SMMA 14 - HMA 24 - 0.11
SMMA 18 - HMA 24 - 0.11
SMA 16 - HMA 28 - 0.04
SMA 24 - HMA 28 - 0.09
SMA 26 - HMA 28 - 0.10
SMA 28 - HMA 28 - 0.10
HMA 8 - JMA 10 - 0.11
LinearReg 4 - JMA 10 - 0.11
LinearReg 8 - JMA 10 - 0.11
SMA 10 - JMA 10 - 0.01
HMA 8 - JMA 12 - 0.11
LinearReg 10 - JMA 12 - 0.11
LinearReg 8 - JMA 12 - 0.10
Lowest 2 - JMA 12 - 0.10
Lowest 8 - JMA 12 - 0.00
SMA 10 - JMA 12 - 0.06
SMA 12 - JMA 12 - 0.09
EMA 10 - JMA 14 - 0.06
EMA 12 - JMA 14 - 0.03
HMA 12 - JMA 14 - 0.09
LinearReg 14 - JMA 14 - 0.11
Lowest 6 - JMA 14 - 0.02
SMMA 10 - JMA 14 - 0.05
SMMA 2 - JMA 14 - 0.08
SMMA 8 - JMA 14 - 0.10
EMA 14 - JMA 16 - 0.07
EMA 16 - JMA 16 - 0.11
EMA 14 - HMA 18 - 0.02
EMA 16 - HMA 18 - 0.03
SMA 16 - HMA 18 - 0.10
SMMA 18 - HMA 18 - 0.05
SMMA 2 - HMA 2 - 0.06
SMMA 14 - HMA 20 - 0.09
SMA 12 - HMA 22 - 0.09
SMA 6 - JMA 16 - 0.08
SMA 8 - JMA 16 - 0.08
SMMA 14 - JMA 16 - 0.10
SMMA 6 - JMA 16 - 0.07
EMA 10 - JMA 18 - 0.08
EMA 6 - JMA 18 - 0.11
EMA 8 - JMA 18 - 0.11
HMA 16 - JMA 18 - 0.09
LinearReg 18 - JMA 18 - 0.05
SMA 4 - JMA 18 - 0.11
SMMA 12 - JMA 18 - 0.11
SMMA 16 - JMA 18 - 0.11
SMMA 8 - JMA 18 - 0.10
EMA 2 - JMA 2 - 0.04
SMMA 2 - JMA 2 - 0.06
EMA 10 - JMA 20 - 0.09
EMA 12 - JMA 20 - 0.09
EMA 6 - JMA 20 - 0.11
SMMA 12 - JMA 20 - 0.10
EMA 10 - JMA 22 - 0.11
EMA 20 - JMA 22 - 0.10
EMA 8 - JMA 22 - 0.11
HMA 16 - JMA 22 - 0.11
LinearReg 18 - JMA 22 - 0.06
SMMA 12 - JMA 22 - 0.10
EMA 14 - JMA 24 - 0.10
EMA 16 - JMA 24 - 0.11
SMMA 14 - HMA 16 - 0.09
SMA 18 - HMA 18 - 0.09
SMMA 10 - HMA 18 - 0.09
SMMA 12 - HMA 18 - 0.09
SMMA 8 - HMA 18 - 0.02
EMA 2 - HMA 2 - 0.09
Highest 2 - HMA 2 - 0.11
SMA 14 - HMA 20 - 0.11
SMA 16 - HMA 20 - 0.10
SMMA 10 - HMA 20 - 0.09
SMA 14 - HMA 22 - 0.09
SMMA 18 - HMA 22 - 0.10
SMMA 16 - HMA 24 - 0.11
SMMA 16 - HMA 18 - 0.09
JMA 2 - HMA 2 - 0.11
SMMA 12 - HMA 20 - 0.08
SMMA 18 - HMA 20 - 0.10
EMA 22 - HMA 22 - 0.08
SMMA 12 - HMA 22 - 0.09
SMMA 22 - HMA 22 - 0.10
SMA 12 - HMA 24 - 0.09
SMMA 14 - HMA 22 - 0.09
SMMA 2 - HMA 4 - 0.03
EMA 6 - HMA 6 - 0.03
SMA 4 - HMA 6 - 0.03
SMA 6 - HMA 8 - 0.03
EMA 6 - JMA 10 - 0.02
EMA 8 - JMA 10 - 0.06
Lowest 2 - JMA 10 - 0.10
SMA 4 - JMA 10 - 0.04
Lowest 2 - JMA 24 - 0.11
SMA 14 - JMA 24 - 0.06
SMA 22 - JMA 24 - 0.09
SMA 8 - JMA 24 - 0.11
SMMA 14 - JMA 24 - 0.11
SMMA 22 - JMA 24 - 0.11
SMMA 6 - JMA 24 - 0.09
LinearReg 24 - JMA 26 - 0.05
Lowest 4 - JMA 26 - 0.08
SMA 10 - JMA 26 - 0.10
SMA 20 - JMA 26 - 0.08
SMMA 16 - JMA 26 - 0.11
SMMA 20 - JMA 26 - 0.11
EMA 12 - JMA 28 - 0.09
EMA 24 - JMA 28 - 0.11
EMA 26 - JMA 28 - 0.11
SMA 14 - JMA 28 - 0.09
SMA 16 - JMA 28 - 0.09
SMA 18 - JMA 28 - 0.09
SMA 24 - JMA 28 - 0.09
SMA 18 - HMA 30 - 0.09
SMA 24 - HMA 30 - 0.10
SMA 26 - HMA 30 - 0.10
EMA 4 - HMA 6 - 0.00
SMA 6 - HMA 6 - 0.03
SMMA 4 - HMA 6 - 0.03
SMMA 6 - HMA 6 - 0.03
EMA 10 - JMA 10 - 0.08
EMA 4 - JMA 10 - 0.02
LinearReg 6 - JMA 10 - 0.11
SMA 2 - JMA 10 - 0.10
SMA 6 - JMA 10 - 0.00
SMA 8 - JMA 10 - 0.09
SMMA 4 - JMA 10 - 0.04
SMMA 6 - JMA 10 - 0.08
LinearReg 6 - JMA 12 - 0.11
SMA 6 - JMA 12 - 0.03
SMA 8 - JMA 12 - 0.07
SMMA 12 - JMA 12 - 0.07
SMMA 6 - JMA 12 - 0.06
EMA 14 - JMA 14 - 0.11
EMA 6 - JMA 12 - 0.02
HMA 10 - JMA 12 - 0.11
Lowest 6 - JMA 12 - 0.00
SMA 4 - JMA 12 - 0.05
EMA 4 - JMA 14 - 0.08
LinearReg 6 - JMA 14 - 0.10
SMA 6 - JMA 14 - 0.06
SMA 8 - JMA 14 - 0.06
SMMA 12 - JMA 14 - 0.07
SMMA 4 - JMA 14 - 0.06
SMMA 6 - JMA 14 - 0.06
EMA 12 - JMA 16 - 0.06
HMA 14 - JMA 16 - 0.11
LinearReg 6 - JMA 16 - 0.11
LinearReg 8 - JMA 16 - 0.11
SMA 14 - JMA 16 - 0.07
SMA 16 - JMA 16 - 0.06
SMMA 10 - JMA 16 - 0.08
HMA 12 - JMA 18 - 0.11
HMA 8 - JMA 18 - 0.11
Lowest 2 - JMA 18 - 0.11
Lowest 4 - JMA 18 - 0.08
SMA 10 - JMA 18 - 0.06
SMMA 2 - JMA 18 - 0.10
EMA 20 - JMA 20 - 0.07
SMA 10 - JMA 20 - 0.06
SMA 12 - JMA 20 - 0.08
SMA 20 - JMA 20 - 0.06
SMMA 16 - JMA 20 - 0.11
SMA 26 - JMA 28 - 0.11
SMA 28 - JMA 28 - 0.11
SMMA 18 - JMA 28 - 0.11
EMA 24 - JMA 30 - 0.11
EMA 26 - JMA 30 - 0.11
EMA 30 - JMA 30 - 0.11
SMMA 24 - JMA 30 - 0.11
EMA 30 - JMA 32 - 0.11
SMMA 8 - JMA 32 - 0.10
SMA 22 - JMA 34 - 0.11
SMA 32 - JMA 34 - 0.10
EMA 6 - JMA 14 - 0.05
EMA 8 - JMA 14 - 0.06
HMA 10 - JMA 14 - 0.11
LinearReg 12 - JMA 14 - 0.10
Lowest 2 - JMA 14 - 0.11
Lowest 4 - JMA 14 - 0.05
Lowest 8 - JMA 14 - 0.00
SMA 14 - JMA 14 - 0.08
SMA 4 - JMA 14 - 0.11
SMMA 14 - JMA 14 - 0.07
HMA 12 - JMA 16 - 0.11
Lowest 2 - JMA 16 - 0.11
SMA 10 - JMA 16 - 0.07
SMMA 2 - JMA 16 - 0.10
EMA 12 - JMA 18 - 0.10
HMA 14 - JMA 18 - 0.11
LinearReg 16 - JMA 18 - 0.07
SMA 14 - JMA 18 - 0.06
SMA 16 - JMA 18 - 0.07
SMA 18 - JMA 18 - 0.07
SMMA 10 - JMA 18 - 0.11
SMMA 18 - JMA 18 - 0.11
SMA 2 - JMA 2 - 0.11
EMA 14 - JMA 20 - 0.10
EMA 16 - JMA 20 - 0.07
HMA 14 - JMA 20 - 0.10
LinearReg 18 - JMA 20 - 0.09
SMA 16 - JMA 20 - 0.08
SMA 18 - JMA 20 - 0.06
SMMA 10 - JMA 20 - 0.10
SMMA 18 - JMA 20 - 0.11
SMMA 20 - JMA 20 - 0.11
SMMA 8 - JMA 20 - 0.10
EMA 18 - JMA 22 - 0.10
LinearReg 20 - JMA 22 - 0.05
LinearReg 22 - JMA 22 - 0.03
Lowest 2 - JMA 22 - 0.11
Lowest 4 - JMA 22 - 0.10
SMA 10 - JMA 22 - 0.10
SMA 12 - JMA 22 - 0.11
SMA 20 - JMA 22 - 0.06
SMMA 16 - JMA 22 - 0.11
SMMA 8 - JMA 22 - 0.10
EMA 12 - JMA 24 - 0.08
LinearReg 16 - JMA 24 - 0.05
SMA 16 - JMA 24 - 0.08
SMA 18 - JMA 24 - 0.09
SMA 24 - JMA 24 - 0.08
SMMA 10 - JMA 24 - 0.11
SMMA 18 - JMA 24 - 0.11
SMMA 20 - JMA 24 - 0.11
EMA 14 - JMA 26 - 0.09
EMA 16 - JMA 26 - 0.09
EMA 18 - JMA 26 - 0.09
SMMA 22 - JMA 34 - 0.11
SMA 20 - JMA 36 - 0.11
SMA 30 - JMA 36 - 0.10
SMA 32 - JMA 36 - 0.11
SMA 16 - JMA 38 - 0.11
SMA 18 - JMA 38 - 0.11
SMA 24 - JMA 38 - 0.10
SMA 26 - JMA 38 - 0.09
SMA 28 - JMA 38 - 0.10
SMA 2 - JMA 4 - 0.06
EMA 12 - JMA 22 - 0.07
EMA 22 - JMA 22 - 0.11
LinearReg 16 - JMA 22 - 0.11
SMA 16 - JMA 22 - 0.06
SMA 18 - JMA 22 - 0.07
SMMA 10 - JMA 22 - 0.10
SMMA 18 - JMA 22 - 0.11
SMMA 20 - JMA 22 - 0.11
EMA 18 - JMA 24 - 0.10
EMA 20 - JMA 24 - 0.08
LinearReg 22 - JMA 24 - 0.06
SMA 10 - JMA 24 - 0.10
SMA 12 - JMA 24 - 0.06
SMA 20 - JMA 24 - 0.10
SMMA 16 - JMA 24 - 0.11
SMMA 8 - JMA 24 - 0.10
EMA 10 - JMA 26 - 0.09
EMA 20 - JMA 26 - 0.11
EMA 22 - JMA 26 - 0.11
SMMA 10 - JMA 26 - 0.10
SMMA 12 - JMA 26 - 0.11
SMMA 22 - JMA 26 - 0.11
SMMA 6 - JMA 26 - 0.09
EMA 14 - JMA 28 - 0.08
EMA 16 - JMA 28 - 0.11
EMA 18 - JMA 28 - 0.10
EMA 28 - JMA 28 - 0.10
LinearReg 26 - JMA 26 - 0.03
Lowest 2 - JMA 26 - 0.09
SMA 12 - JMA 26 - 0.08
SMA 22 - JMA 26 - 0.11
SMMA 14 - JMA 26 - 0.11
SMMA 24 - JMA 26 - 0.11
SMMA 8 - JMA 26 - 0.11
EMA 20 - JMA 28 - 0.09
EMA 22 - JMA 28 - 0.11
SMMA 10 - JMA 28 - 0.09
SMMA 12 - JMA 28 - 0.11
SMMA 22 - JMA 28 - 0.11
SMMA 6 - JMA 28 - 0.09
SMA 30 - JMA 30 - 0.11
SMMA 16 - JMA 30 - 0.11
SMA 26 - JMA 40 - 0.10
SMA 28 - JMA 40 - 0.10
SMA 26 - JMA 42 - 0.09
SMA 28 - JMA 42 - 0.10
Lowest 2 - JMA 28 - 0.10
SMA 12 - JMA 28 - 0.09
SMA 22 - JMA 28 - 0.08
SMMA 14 - JMA 28 - 0.11
SMMA 24 - JMA 28 - 0.11
SMMA 8 - JMA 28 - 0.08
EMA 28 - JMA 30 - 0.11
Lowest 6 - JMA 30 - 0.06
SMA 24 - JMA 30 - 0.11
SMA 26 - JMA 30 - 0.11
SMA 28 - JMA 30 - 0.11
SMMA 18 - JMA 30 - 0.11
SMMA 20 - JMA 30 - 0.11
SMA 20 - JMA 32 - 0.08
SMA 30 - JMA 32 - 0.11
SMMA 16 - JMA 32 - 0.11
SMA 16 - JMA 34 - 0.09
SMA 18 - JMA 34 - 0.10
SMA 24 - JMA 34 - 0.11
SMA 26 - JMA 34 - 0.11
SMA 28 - JMA 34 - 0.10
SMA 34 - JMA 34 - 0.11
SMMA 20 - JMA 34 - 0.11
SMA 2 - HMA 4 - 0.01
SMMA 10 - JMA 10 - 0.07
SMMA 2 - JMA 10 - 0.00
SMMA 8 - JMA 10 - 0.07
EMA 10 - JMA 12 - 0.06
EMA 12 - JMA 12 - 0.11
SMMA 10 - JMA 12 - 0.05
SMMA 2 - JMA 12 - 0.04
SMMA 8 - JMA 12 - 0.06
HMA 14 - JMA 14 - 0.11
HMA 8 - JMA 14 - 0.11
LinearReg 8 - JMA 14 - 0.11
SMA 10 - JMA 14 - 0.06
SMA 12 - JMA 14 - 0.07
EMA 10 - JMA 16 - 0.06
SMA 12 - JMA 44 - 0.10
SMA 30 - JMA 44 - 0.11
SMMA 2 - JMA 6 - 0.02
SMMA 2 - JMA 8 - 0.02
SMA 18 - JMA 32 - 0.10
SMA 24 - JMA 32 - 0.11
SMA 26 - JMA 32 - 0.11
SMA 28 - JMA 32 - 0.11
SMMA 18 - JMA 32 - 0.11
SMMA 20 - JMA 32 - 0.11
SMA 20 - JMA 34 - 0.11
SMA 30 - JMA 34 - 0.10
SMA 22 - JMA 36 - 0.11
LinearReg 4 - JMA 4 - 0.11
EMA 6 - JMA 16 - 0.06
EMA 8 - JMA 16 - 0.08
HMA 10 - JMA 16 - 0.11
SMMA 12 - JMA 16 - 0.08
SMMA 16 - JMA 16 - 0.10
SMMA 8 - JMA 16 - 0.10
EMA 14 - JMA 18 - 0.07
EMA 16 - JMA 18 - 0.08
EMA 18 - JMA 18 - 0.11
EMA 4 - JMA 18 - 0.11
HMA 18 - JMA 18 - 0.09
LinearReg 6 - JMA 18 - 0.11
LinearReg 8 - JMA 18 - 0.11
Lowest 6 - JMA 18 - 0.01
Lowest 8 - JMA 18 - 0.00
SMA 12 - JMA 18 - 0.06
SMA 6 - JMA 18 - 0.11
SMA 8 - JMA 18 - 0.10
SMMA 14 - JMA 18 - 0.09
SMMA 4 - JMA 18 - 0.11
SMMA 6 - JMA 18 - 0.07
Lowest 2 - JMA 2 - 0.06
EMA 18 - JMA 20 - 0.07
HMA 18 - JMA 20 - 0.11
Lowest 2 - JMA 20 - 0.11
SMA 14 - JMA 20 - 0.05
SMA 6 - JMA 20 - 0.11
SMMA 14 - JMA 20 - 0.10
SMMA 6 - JMA 20 - 0.11
EMA 14 - JMA 22 - 0.08
EMA 16 - JMA 22 - 0.10
SMA 14 - JMA 22 - 0.08
SMA 6 - LinearReg 12 - 0.02
EMA 12 - LinearReg 14 - 0.10
SMMA 12 - LinearReg 14 - 0.09
SMMA 8 - LinearReg 14 - 0.09
SMMA 16 - LinearReg 16 - 0.09
HMA 18 - LinearReg 18 - 0.10
SMA 12 - LinearReg 18 - 0.09
JMA 2 - LinearReg 2 - 0.11
SMMA 2 - LinearReg 2 - 0.06
EMA 10 - LinearReg 20 - 0.10
HMA 20 - LinearReg 20 - 0.11
SMMA 10 - LinearReg 20 - 0.11
SMMA 12 - LinearReg 20 - 0.10
HMA 22 - LinearReg 22 - 0.11
SMA 12 - LinearReg 24 - 0.10
SMA 22 - LinearReg 24 - 0.10
SMMA 14 - LinearReg 24 - 0.10
SMMA 16 - LinearReg 24 - 0.11
SMMA 22 - LinearReg 24 - 0.11
SMMA 10 - LinearReg 26 - 0.11
SMA 22 - JMA 38 - 0.11
SMA 30 - JMA 38 - 0.10
SMA 32 - JMA 38 - 0.11
Lowest 2 - JMA 4 - 0.10
SMMA 2 - JMA 4 - 0.01
SMA 24 - JMA 40 - 0.11
SMA 18 - LinearReg 30 - 0.11
SMA 26 - LinearReg 30 - 0.11
SMA 24 - JMA 42 - 0.11
EMA 6 - JMA 6 - 0.00
SMA 4 - JMA 6 - 0.03
SMA 2 - JMA 8 - 0.10
SMA 6 - JMA 8 - 0.00
EMA 10 - LinearReg 14 - 0.11
EMA 14 - LinearReg 16 - 0.09
EMA 16 - LinearReg 16 - 0.09
SMA 14 - LinearReg 16 - 0.10
SMMA 14 - LinearReg 16 - 0.09
JMA 32 - LinearReg 34 - 0.07
JMA 34 - LinearReg 34 - 0.07
SMA 14 - LinearReg 36 - 0.11
SMA 16 - LinearReg 38 - 0.11
EMA 4 - LinearReg 4 - 0.03
EMA 6 - LinearReg 6 - 0.03
SMA 4 - LinearReg 6 - 0.03
MeanDeviatio - Momentum 10 - 0.03
MeanDeviatio - Momentum 10 - 0.01
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
SMA 14 - LinearReg 18 - 0.09
SMA 16 - LinearReg 18 - 0.09
SMA 2 - LinearReg 2 - 0.11
EMA 20 - LinearReg 20 - 0.11
SMMA 16 - LinearReg 20 - 0.10
SMA 12 - LinearReg 22 - 0.04
SMA 22 - LinearReg 22 - 0.09
EMA 14 - LinearReg 24 - 0.10
EMA 16 - LinearReg 24 - 0.11
EMA 18 - LinearReg 24 - 0.11
SMA 14 - LinearReg 24 - 0.10
SMA 2 - JMA 6 - 0.11
SMA 6 - JMA 6 - 0.03
SMMA 4 - JMA 6 - 0.02
EMA 6 - JMA 8 - 0.04
HMA 12 - LinearReg 12 - 0.06
EMA 14 - LinearReg 14 - 0.09
SMMA 10 - LinearReg 14 - 0.09
SMMA 12 - LinearReg 16 - 0.09
SMMA 6 - LinearReg 16 - 0.08
MeanDeviatio - Momentum 16 - 0.10
MeanDeviatio - Momentum 18 - 0.09
MeanDeviatio - Momentum 18 - 0.05
Momentum 6 - Momentum 18 - 0.10
MeanDeviatio - Momentum 20 - 0.10
MeanDeviatio - Momentum 20 - 0.09
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 26 - 0.10
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.10
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
Momentum 12 - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
SMA 22 - JMA 22 - 0.08
SMA 6 - JMA 22 - 0.11
SMA 8 - JMA 22 - 0.10
SMMA 14 - JMA 22 - 0.10
SMMA 22 - JMA 22 - 0.11
SMMA 6 - JMA 22 - 0.11
EMA 10 - JMA 24 - 0.08
EMA 22 - JMA 24 - 0.10
EMA 24 - JMA 24 - 0.11
LinearReg 18 - JMA 24 - 0.07
SMMA 12 - JMA 24 - 0.10
SMMA 24 - JMA 24 - 0.11
EMA 12 - JMA 26 - 0.09
EMA 24 - JMA 26 - 0.11
EMA 26 - JMA 26 - 0.11
SMA 14 - JMA 26 - 0.09
SMA 16 - JMA 26 - 0.08
SMA 18 - JMA 26 - 0.08
SMA 24 - JMA 26 - 0.08
SMA 26 - JMA 26 - 0.11
SMMA 18 - JMA 26 - 0.11
SMA 10 - JMA 28 - 0.06
SMA 20 - JMA 28 - 0.11
SMMA 20 - LinearReg 24 - 0.11
EMA 24 - LinearReg 26 - 0.11
EMA 26 - LinearReg 26 - 0.11
SMA 8 - LinearReg 26 - 0.11
SMMA 12 - LinearReg 26 - 0.11
SMMA 18 - LinearReg 26 - 0.11
SMMA 22 - LinearReg 26 - 0.11
SMA 8 - LinearReg 28 - 0.11
SMA 8 - LinearReg 30 - 0.09
EMA 2 - LinearReg 2 - 0.06
EMA 12 - LinearReg 20 - 0.09
SMA 14 - LinearReg 20 - 0.09
SMA 16 - LinearReg 20 - 0.09
SMA 18 - LinearReg 20 - 0.09
SMMA 18 - LinearReg 20 - 0.11
EMA 20 - LinearReg 24 - 0.11
EMA 22 - LinearReg 24 - 0.10
SMA 20 - LinearReg 24 - 0.11
EMA 14 - LinearReg 26 - 0.11
EMA 16 - LinearReg 26 - 0.10
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 4 - 0.02
Momentum 2 - Momentum 4 - 0.00
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 6 - 0.03
SMMA 16 - JMA 28 - 0.11
SMMA 20 - JMA 28 - 0.11
Lowest 2 - JMA 30 - 0.11
Lowest 4 - JMA 30 - 0.11
Lowest 8 - JMA 30 - 0.04
SMA 22 - JMA 30 - 0.10
SMMA 14 - JMA 30 - 0.11
SMMA 22 - JMA 30 - 0.11
EMA 16 - JMA 32 - 0.08
EMA 28 - JMA 32 - 0.11
SMA 22 - JMA 32 - 0.11
SMA 32 - JMA 32 - 0.10
SMMA 22 - JMA 32 - 0.11
SMA 14 - LinearReg 34 - 0.11
SMA 26 - LinearReg 34 - 0.11
JMA 32 - LinearReg 36 - 0.07
JMA 34 - LinearReg 36 - 0.07
SMMA 4 - LinearReg 4 - 0.03
KAMA 2 - LinearReg 6 - 0.03
SMA 12 - LinearReg 26 - 0.11
SMMA 14 - LinearReg 26 - 0.11
SMMA 8 - LinearReg 26 - 0.10
SMA 16 - JMA 36 - 0.10
SMA 18 - JMA 36 - 0.11
SMA 26 - JMA 36 - 0.09
SMA 28 - JMA 36 - 0.10
SMA 20 - JMA 38 - 0.10
SMA 4 - JMA 4 - 0.03
SMA 14 - LinearReg 30 - 0.11
SMA 16 - LinearReg 30 - 0.10
SMA 24 - LinearReg 30 - 0.11
JMA 32 - LinearReg 32 - 0.07
JMA 36 - LinearReg 38 - 0.08
EMA 2 - LinearReg 4 - 0.01
SMA 22 - JMA 40 - 0.10
SMA 30 - JMA 40 - 0.10
SMA 30 - JMA 42 - 0.10
MeanDeviatio - Momentum 10 - 0.01
MeanDeviatio - Momentum 12 - 0.04
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
Momentum 12 - Momentum 14 - 0.11
MeanDeviatio - Momentum 16 - 0.09
MeanDeviatio - Momentum 18 - 0.07
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 20 - 0.09
MeanDeviatio - Momentum 20 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
SMA 28 - JMA 44 - 0.10
EMA 4 - JMA 6 - 0.00
LinearReg 4 - JMA 6 - 0.11
LinearReg 6 - JMA 6 - 0.11
Lowest 2 - JMA 6 - 0.11
SMMA 6 - JMA 6 - 0.03
EMA 4 - JMA 8 - 0.02
LinearReg 4 - JMA 8 - 0.11
LinearReg 6 - JMA 8 - 0.10
LinearReg 8 - JMA 8 - 0.10
Lowest 2 - JMA 8 - 0.11
Momentum 12 - Momentum 22 - 0.10
Momentum 8 - Momentum 22 - 0.11
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 10 - 0.02
MeanDeviatio - Momentum 12 - 0.04
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 42 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 6 - 0.03
SMMA 12 - SMA 12 - 0.11
MeanDeviatio - Momentum 16 - 0.09
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 18 - 0.09
MeanDeviatio - Momentum 18 - 0.09
MeanDeviatio - Momentum 20 - 0.10
MeanDeviatio - Momentum 20 - 0.05
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.09
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 24 - 0.10
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
Momentum 10 - Momentum 28 - 0.11
Momentum 12 - Momentum 28 - 0.10
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
Momentum 26 - Momentum 38 - 0.08
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 8 - 0.01
SMA 8 - JMA 8 - 0.00
SMMA 6 - JMA 8 - 0.09
SMMA 14 - LinearReg 14 - 0.04
SMMA 6 - LinearReg 14 - 0.03
EMA 10 - LinearReg 16 - 0.09
EMA 12 - LinearReg 16 - 0.09
SMA 16 - LinearReg 16 - 0.10
SMMA 10 - LinearReg 16 - 0.09
SMMA 8 - LinearReg 16 - 0.09
SMA 18 - LinearReg 18 - 0.09
Lowest 2 - LinearReg 2 - 0.11
EMA 18 - LinearReg 20 - 0.11
HMA 18 - LinearReg 20 - 0.11
SMA 12 - LinearReg 20 - 0.09
SMMA 14 - LinearReg 20 - 0.10
SMMA 20 - LinearReg 20 - 0.11
SMMA 6 - LinearReg 20 - 0.06
SMA 14 - LinearReg 22 - 0.09
SMA 16 - LinearReg 22 - 0.11
EMA 12 - LinearReg 24 - 0.10
EMA 24 - LinearReg 24 - 0.10
SMMA 10 - LinearReg 24 - 0.10
SMMA 12 - LinearReg 24 - 0.10
SMMA 18 - LinearReg 24 - 0.11
SMMA 24 - LinearReg 24 - 0.11
SMMA 8 - LinearReg 24 - 0.10
EMA 18 - LinearReg 26 - 0.10
EMA 20 - LinearReg 26 - 0.09
EMA 22 - LinearReg 26 - 0.09
HMA 26 - LinearReg 26 - 0.09
SMA 20 - LinearReg 26 - 0.10
SMMA 16 - LinearReg 26 - 0.11
SMMA 20 - LinearReg 26 - 0.11
SMA 12 - LinearReg 28 - 0.10
SMA 12 - LinearReg 30 - 0.11
SMA 14 - LinearReg 32 - 0.11
SMA 16 - LinearReg 32 - 0.11
SMA 24 - LinearReg 32 - 0.11
JMA 36 - LinearReg 36 - 0.07
SMMA 2 - LinearReg 4 - 0.02
SMA 2 - LinearReg 6 - 0.00
SMA 6 - LinearReg 6 - 0.03
SMMA 4 - LinearReg 6 - 0.03
SMMA 6 - LinearReg 6 - 0.03
MeanDeviatio - Momentum 10 - 0.03
MeanDeviatio - Momentum 12 - 0.03
MeanDeviatio - Momentum 14 - 0.10
MeanDeviatio - Momentum 16 - 0.09
Momentum 12 - Momentum 16 - 0.10
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 18 - 0.05
MeanDeviatio - Momentum 2 - 0.06
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.10
MeanDeviatio - Momentum 22 - 0.11
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 24 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 26 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.11
MeanDeviatio - Momentum 28 - 0.10
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 30 - 0.11
MeanDeviatio - Momentum 32 - 0.11
MeanDeviatio - Momentum 32 - 0.10
MeanDeviatio - Momentum 32 - 0.10
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 34 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 36 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 38 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.11
MeanDeviatio - Momentum 40 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.10
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 42 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.10
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 44 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 46 - 0.11
MeanDeviatio - Momentum 6 - 0.02
MeanDeviatio - Momentum 8 - 0.02";
        #endregion
        
    }
}
