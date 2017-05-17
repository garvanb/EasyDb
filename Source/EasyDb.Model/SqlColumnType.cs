using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{
    public enum SqlColumnType
    {
        Undefined,
        bigint = 1,
        binary50 = 2,
        bit = 3,
        char10 = 4,
        datetime = 5,
        decimal180 = 6,
        Float = 7,
        image = 8,
        Int = 9,
        money = 10,
        nchar10 = 11,
        ntext = 12,
        numeric180 = 13,
        nvarchar50 = 14,
        nvarcharMAX = 15,
        real = 16,
        smalldatetime = 17,
        smallint = 18,
        smallmoney = 19,
        sql_variant = 20,
        text = 21,
        timestamp = 22,
        tinyint = 23,
        uniqueidentifier = 24,
        varbinary50 = 25,
        varbinaryMAX = 26,
        varchar50 = 27,
        varcharMAX = 28,
        xml = 29
    }


}
