ffmpeg -i ./three-new-new.mov -vf palettegen -y ./three-new-new.png

ffmpeg -i ./three-new-new.mov -i ./three-new-new.png -lavfi paletteuse -y ./three-new-new.gif
