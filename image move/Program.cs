using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace image_move
{
    class Image
    {
        public int totalCount = 0;
        public int jpgCount = 0;
        public int pngCount = 0;
        public int failCount = 0;
        public void Move(string a)
        {
            try {
                string Dir = a;

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Dir);
                foreach (System.IO.FileInfo File in di.GetFiles())
                {
                    try
                    {
                        string fPath2 = "";
                        switch (File.Extension.ToLower())
                        {
                            case ".jpg":
                                using (var image1 = new Bitmap(File.FullName))
                                {
                                    SizeCheckJpg(image1, Dir, File.Name, out fPath2);
                                }
                                File.MoveTo(fPath2);
                                jpgCount++;
                                break;

                            case ".png":
                                using (var image1 = new Bitmap(@File.FullName))
                                {
                                    SizeCheckPng(image1, Dir, File.Name, out fPath2);
                                }
                                File.MoveTo(fPath2);
                                pngCount++;
                                break;

                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        failCount++;
                    }
                    totalCount++;
                }
                Console.WriteLine("총 {0}개의 파일중 jpg : {1}, png : {2}, 실패 : {3}", totalCount, jpgCount, pngCount,failCount);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.GetType());
            }
        }

        public void SizeCheckJpg(Bitmap send, string path1, string fileName, out string fP2)
        {
            string movePath = path1;

            if ((send.Width >= 5000)&&(send.Height>=5000))
            {
                movePath += @"\jpg 5000\" + fileName;
                fP2 = movePath;
            }
            else if ((send.Width >= 4000) && (send.Height >= 4000))
            {
                movePath += @"\jpg 4000\" + fileName;
                fP2 = movePath;
            }
            else if ((send.Width >= 2000) && (send.Height >= 2000))
            {
                movePath += @"\jpg 2000\" + fileName;
                fP2 = movePath;
            }
            else
            {
                movePath += @"\jpg 0\" + fileName;
                fP2 = movePath;
            }
        }
        public void SizeCheckPng(Bitmap send, string path1, string fileName, out string fP2)
        {
            string movePath = path1;
            if ((send.Width >= 5000) && (send.Height >= 5000))
            {
                movePath += @"\png 5000\" + fileName;
                fP2 = movePath;
            }
            else if ((send.Width >= 4000) && (send.Height >= 4000))
            {
                movePath += @"\png 4000\" + fileName;
                fP2 = movePath;
            }
            else if ((send.Width >= 2000) && (send.Height >= 2000))
            {
                movePath += @"\png 2000\" + fileName;
                fP2 = movePath;
            }
            else
            {
                movePath += @"\png 0\" + fileName;
                fP2 = movePath;
            }
        }
    }
    class Program
    {
        class CreateFolder
        {
            public void Create(string a)
            {
                string dir = a;
                string p1 = @"\jpg ";
                string p2 = @"\png ";
                string emse = @"";
                for (int x = 0; x < 2; x++)
                {
                    if (x == 0)
                    {
                        emse = p1;
                    }
                    else if (x == 1)
                    {
                        emse = p2;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        int j = 0;
                        switch (i)
                        {
                            case 0:
                                j = 0;
                                break;
                            case 1:
                                j = 2000;
                                break;
                            case 2:
                                j = 4000;
                                break;
                            case 3:
                                j = 5000;
                                break;
                            default:
                                break;
                        }
                        DirectoryInfo createDir = new DirectoryInfo(dir + emse + j);
                        if (createDir.Exists == false)
                        {
                            createDir.Create();
                        }
                    }
                }
            }
        }

        class Car
        {
            public int a = 0;
            public int b = 1;

        }
        static void Main(string[] args)
        {
            int inputKey;
            string a;
            bool start = true;
            Image image1 = new Image();
            string pDir = null;
            List<Car> cars = new List<Car>();

            for (int i = 0; i < 10; i++)
            {
                Car car = new Car();
                Console.WriteLine(i+"번");
                cars.Add(car);
            }
            Console.WriteLine(cars.Count);

            foreach(var item in cars)
            {
                Console.WriteLine(item.a);
            }
            while (start)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("1 : 경로입력  |  2 : 폴더 생성  |  3 : 파일 이동  |  그외숫자 : 종료");
                Console.Write("숫자입력(1,2,3) : ");
                a = Console.ReadLine();
                try
                {
                    inputKey = int.Parse(a);

                    switch (inputKey)
                    {
                        case 1:
                            Console.Write("경로를 입력 : ");
                            pDir = Console.ReadLine();
                            Console.WriteLine("입력한 경로는 : " + pDir);
                            break;
                        case 2:
                            if (pDir != null)
                            {
                                CreateFolder create1 = new CreateFolder();  //폴더 생성
                                create1.Create(pDir);
                                Console.WriteLine("폴더 생성 성공");
                            }
                            else
                            {
                                Console.WriteLine("경로 값이 없습니다.");
                            }
                            break;
                        case 3:
                            if (pDir != null)
                            {
                                image1.Move(pDir);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("경로에 값이 없습니다.");
                                break;
                            }
                        default:
                            start = false;
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.GetType());
                    Console.WriteLine("while문에서 오류발생");
                    start = false;  //asd
                }
            }
        }
    }
}