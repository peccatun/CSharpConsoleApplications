using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Room
{
    private readonly IEnumerable<Box> boxes;
    private Prisoner prisoner;

    private int succeded;
    private int failed;

    public Room(IEnumerable<Box> boxes)
    {
        this.boxes = boxes;
        succeded = 0;
        failed = 0;
    }

    public int Succeeded => succeded;

    public int Failed => failed;

    public Prisoner Prisoner
    { 
        get 
        {
            return prisoner;
        } 
        set
        {
            prisoner = value;
            prisoner.BoxFound += OnBoxFound;
            prisoner.BoxNotFound += OnBoxNotFound;
        }
    }

    public void Search()
    {

        Prisoner.OpenBoxes(boxes);
        CloseBoxes();
    }

    public void SearchModel() 
    {
        prisoner.CheckNeededSearches(boxes);
        CloseBoxes();
        Console.WriteLine("Searches needed: " + prisoner.GetSeachesNeeded());
        Prisoner.OpenBoxesAdequate(boxes);
        CloseBoxes();
    }

    public void Reset() 
    {
        succeded = 0;
        failed = 0;
    }

    public string GetResults() 
    {
        return $"Succeeded: {succeded} Failed: {failed}";
    }

    private void OnBoxFound(object sender, EventArgs args)
    {
        using Prisoner prisoner = (Prisoner)sender;
        prisoner.HasFound = true;
        succeded++;
        Console.WriteLine($"Prisoner: {prisoner.Number} succeeded: {prisoner.GetBoxes()} {Environment.NewLine} Checked: {prisoner.GetBoxesCount()}  Open: {boxes.Where(b => b.IsOpened).Count()}");
    }

    private void OnBoxNotFound(object sender, EventArgs args)
    {
        failed++;
        using Prisoner prisoner = (Prisoner)sender;
        prisoner.HasFound = false;
        Console.WriteLine($"Prisoner: {prisoner.Number} failed: {prisoner.GetBoxes()} {Environment.NewLine} Checked: {prisoner.GetBoxesCount()} Open: {boxes.Where(b => b.IsOpened).Count()}");
        
    }

    private void CloseBoxes() 
    {
        for (int i = 0; i < boxes.Count(); i++)
        {
            boxes.ElementAt(i).IsOpened = false;
        }
    }
}