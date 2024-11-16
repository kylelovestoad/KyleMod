module KyleModSampleTrigger

using ..Ahorn, Maple

@mapdef Trigger "KyleMod/SampleTrigger" SampleTrigger(
    x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight,
    sampleProperty::Integer=0
)

const placements = Ahorn.PlacementDict(
    "Sample Trigger (KyleMod)" => Ahorn.EntityPlacement(
        SampleTrigger,
        "rectangle",
    )
)

end