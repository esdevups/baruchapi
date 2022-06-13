namespace Baruch.Website.Data
{
    class test
    {
        //Create a method that receives 6 inputs, multiply each number by a random number 6 times and return it to the list
        public static List<int> test1(int a, int b, int c, int d, int e, int f)
        {
            List<int> list = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < 6; i++)
            {
                list.Add(rnd.Next(1, 10));
            }
            list[0] = a * list[0];
            list[1] = b * list[1];
            list[2] = c * list[2];
            list[3] = d * list[3];
            list[4] = e * list[4];
            list[5] = f * list[5];
            return list;
        }

    }


    





}

