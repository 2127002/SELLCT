using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor04Answer01
{
    E30_Name _e30;
    E11_Vivid _e11;
    E17_Number _e17;
    E18_Kanji _e18;
    E19_Hiragana _e19;
    E20_Katakana _e20;
    E21_Alphabet _e21;
    E23_Crosshair _e23;

    public Floor04Answer01(E30_Name e30, E11_Vivid e11, E17_Number e17, E18_Kanji e18, E19_Hiragana e19, E20_Katakana e20, E21_Alphabet e21, E23_Crosshair e23)
    {
        _e30 = e30;
        _e11 = e11;
        _e17 = e17;
        _e18 = e18;
        _e19 = e19;
        _e20 = e20;
        _e21 = e21;
        _e23 = e23;
    }

    private bool HasElements()
    {
        return _e30.ContainsPlayerDeck && _e11.FindAll == 0 && StringElementsType() && _e23.ContainsPlayerDeck;
    }

    private bool StringElementsType()
    {
        //現状は全部必要にしておく。
        return _e17.ContainsPlayerDeck && _e18.ContainsPlayerDeck && _e19.ContainsPlayerDeck && _e20.ContainsPlayerDeck && _e21.ContainsPlayerDeck;
    }
}
