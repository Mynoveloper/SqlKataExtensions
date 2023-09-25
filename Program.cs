using SqlExtensionsTester.Examples;
using SqlExtensionsTester.SqlKataExtensions;
using SqlKata;
using SqlKata.Compilers;



List<Car> cars = new List<Car>
{
                new Car
                {
                    Size = 10,
                    Color = "red",
                    Placas = null,

                },
                new Car
                {
                    Size = 12,
                    Color = "yellow",
                    Placas = "123s123",

                },
                new Car
                {
                    Size = 12,
                    Color = "yellow",
                    Placas = "123s123",

                },
                new Car
                {
                    Size = 10,
                    Color = "red",
                    Placas = "123123",

                },
                new Car
                {
                    Size = 12,
                    Color = "yellow",
                    Placas = "123s123",

                },
                new Car
                {
                    Size = 12,
                    Color = "yellow",
                    Placas = null,

                }
};

Car car = new Car
{
    Size = 12,
    Color = "yellow",
    Placas = "23423fxe#",

};

Query query = new Query();
query.Add(car);
//query.From<Car>();
query.AddMany(cars);
SqlServerCompiler compiler = new SqlServerCompiler();
var sqlresult = compiler.Compile(query);


var x = 1;
