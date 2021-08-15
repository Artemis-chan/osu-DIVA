for %%Z in (%*)do ffmpeg -i %%Z -vf "scale=500:500:force_original_aspect_ratio=decrease,pad=500:500:(ow-iw)/2:(oh-ih)/2:color=00000000,setsar=1" %%Z.png && move "%%~Z.png" %%Z
pause