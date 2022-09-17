using System;
using System.Linq;
using System.Collections.Generic;

public sealed class Prisoner : IDisposable
{
    private const int Start = 1;

    private readonly Random random;
    private readonly List<Box> boxOpened;
    private int seachesNeeded;

    public Prisoner(Random random)
    {
        this.random = random;
        boxOpened = new List<Box>();
        seachesNeeded = 0;
    }

    public event EventHandler BoxFound;
    public event EventHandler BoxNotFound;

    public int Number { get; set; }

    public bool HasFound { get; set; }

    public override string ToString()
    {
        return Number.ToString();
    }

    public int GetSeachesNeeded() 
    {
        return seachesNeeded;
    }

    public void OpenBoxes(IEnumerable<Box> boxes)
    {
        int count = boxes.Count();
        
        for (int i = 0; i < 50; i++)
        {
            bool hasOpened = false;
            while (!hasOpened)
            {
                int boxChoise = random.Next(Start, count + 1);
                Box box = boxes.Where(b => b.Number == boxChoise).First();
                if (box.IsOpened)
                {
                    continue;
                }

                if (boxOpened.Any(b => b.Number == boxChoise))
                {
                    continue;
                }
                box.IsOpened = true;
                boxOpened.Add(box);
                int content = box.Content;

                if (content == Number)
                {
                    BoxFound?.Invoke(this, null);
                    return;
                }
                hasOpened = true;
            }
        }

        BoxNotFound?.Invoke(this, null);
    }

    public void OpenBoxesAdequate(IEnumerable<Box> boxes) 
    {
        bool hasStarted = false;
        int lastOpenedContent = -1; 
        for (int i = 0; i < 50; i++)
        {
            Box box = boxes.Where(b => b.Number == Number).First();

            if (hasStarted)
            {
                box = boxes.Where(b => b.Number == lastOpenedContent).First();
            }


            hasStarted = true;
            int content = box.Content;
            lastOpenedContent = content;

            boxOpened.Add(box);
            box.IsOpened = true;

            if (content == Number)
            {
                BoxFound?.Invoke(this, null);
                return;
            }
        }

        BoxNotFound?.Invoke(this, null);
    }

    public void CheckNeededSearches(IEnumerable<Box> boxes)
    {
        bool hasStarted = false;
        int lastOpenedContent = -1;
        for (int i = 0; i < boxes.Count(); i++)
        {
            Box box = boxes.Where(b => b.Number == Number).First();

            if (hasStarted)
            {
                box = boxes.Where(b => b.Number == lastOpenedContent).First();
            }


            hasStarted = true;
            int content = box.Content;
            lastOpenedContent = content;

            box.IsOpened = true;
            seachesNeeded++;
            if (content == Number)
            {
                return;
            }
        }

        BoxNotFound?.Invoke(this, null);
    }

    public void Reset() 
    {
        boxOpened.Clear();
        HasFound = false;
    }

    public string GetBoxes() 
    {
        return string.Join("| ", boxOpened);
    }

    public int GetBoxesCount() 
    {
        return boxOpened.Count();
    }

    public void Dispose()
    {
        BoxFound = null;
        BoxNotFound = null;
    }
}