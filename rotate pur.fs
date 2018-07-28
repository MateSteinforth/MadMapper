/*{
	"DESCRIPTION": "Your shader description",
	"CREDIT": "by you",
	"CATEGORIES": [
		"Your category"
	],
	"INPUTS": [
		{
			"NAME": "iChannel0",
			"TYPE": "image"
		},
        {
            "NAME": "scale",
            "TYPE": "float",
            "MIN": 0.5,
            "MAX": 3.0,
            "DEFAULT": 1.0
        },
        {
            "NAME": "speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 3.0,
            "DEFAULT": 1.0
        },
      	],
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);
float iTime = TIME;

// The MIT License
// Copyright Â© 2017 Michael Schuresko
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // Normalized pixel coordinates (from 0 to 1)
    vec2 c = (2.0 * fragCoord.xy - iResolution.xy) / iResolution.y;

    /**
     * uncomment as many or as few of the following lines as you like
     *
     * Or add your own of the form
     * c = some_function_of(c)
     */
    // c = vec2(c.x * c.x - c.y * c.y, 2.0 * c.x * c.y);
    // c = 8.0 * mat2(0.96, 0.28, -0.28, 0.96) * c;
    c = mat2(cos(iTime*speed), sin(iTime*speed), -sin(iTime*speed), cos(iTime*speed)) * c / scale;
    // c = c + 0.1 * c * sin(8.0 * (length(c) + iTime));
    // c = 4.0 * c;
    /**
     * end of things to uncomment
     */
    
    // c = mod(abs(mod(0.5 * (1.0 + c), vec2(2.0)) - vec2(1.0)), vec2(1.0));
    
    fragColor = texture(iChannel0, 0.5 * (1.0 + c));
}

void main(void) {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}