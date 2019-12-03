using LanguageExt;
using LanguageExt.ClassInstances;
using System;
using static LanguageExt.Prelude;

namespace Monads
{
    class Program
    {
        static void Main(string[] args)
        {

            Option<int> GetValue() => None;

            var optionalInt = Some(100);

            var noneInt = GetValue();

            int a = noneInt.Match(
                Some: v => v * 2,
                None: () => 0
                );

            int b = match(optionalInt,
                   Some: v => v * 2,
                   None: () => 0);

            int c = optionalInt
              .Some(v => v * 2)
              .None(() => 0);


            int d = noneInt.IfNone(10);
            int e = ifNone(optionalInt, 10);
            ifSome(optionalInt, x => Console.WriteLine(x));


            Option<string> GetStringNone()
            {
                string nullStr = null;
                return Optional(nullStr);
            }

            var optionString = GetStringNone();

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);
            Unsafe();
            Select();

            Console.Read();
        }

        static void Unsafe()
        {
            Console.WriteLine("========= Test Unsafe =========");


            Option<string> GetStringNone() => Optional((string)null);

            var optional = GetStringNone();


            string x = matchUnsafe(optional,
                           Some: v => v,
                           None: () => null);

            Console.WriteLine(x);

            x = ifNoneUnsafe(optional, (string)null);

            Console.WriteLine(x);
        }

        static void Select()
        {
            Console.WriteLine("========= Test SELECT =========");

            Option<int> two = Some(2);
            Option<int> four = Some(4);
            Option<int> six = Some(6);
            Option<int> none = None;

            
            int r = match(from x in two
                          from y in four
                          from z in six
                          select x + y + z,
                           Some: v => v * 2,
                           None: () => 0);     // r == 24

            int rx = match(from x in two
                          from y in four
                          from _ in none
                          from z in six
                          select x + y + z,
                           Some: v => v * 2,
                           None: () => 0);     // r == 0

            Console.WriteLine(r);
            Console.WriteLine(rx);
        }
    }
}
