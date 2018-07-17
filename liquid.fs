/*{
	"DESCRIPTION": "liquid",
	"CREDIT": "shadertoy.com/user/Makio64",
	"CATEGORIES": [
		"mind expansion"
	],
	"INPUTS": [
		{
			"NAME": "iChannel2",
			"TYPE": "image"
		},
    {
        "NAME": "swag1",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 20.0,
        "DEFAULT": 1.0
    },
    {
        "NAME": "swag2",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 2.0
    },
    {
        "NAME": "swag3",
        "TYPE": "float",
        "MIN": 0.1,
        "MAX": 2.0,
        "DEFAULT": 0.5
    },        
  	],
	"IMPORTED": [
		{
			"NAME": "iChannel0",
			"PATH": "10eb4fe0ac8a7dc348a2cc282ca5df1759ab8bf680117e4047728100969e7b43.jpg"
		},
      		{
			"NAME": "iChannel1",
			"PATH": "95b90082f799f48677b4f206d856ad572f1d178c676269eac6347631d4447258.jpg"
		},
      	]
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);
float iTime = TIME;

lowp float snoise(in lowp vec2 v);

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    uv /= 1.1;
    uv += .05;
    uv *= 1.;
	float t = iTime*.8;
	float s = smoothstep(0,1.,uv.x);
    uv.y += s * sin(t+uv.x * swag1) * swag3;// * snoise(uv*2.+1.+t*.3);
    uv.x += s * snoise(uv*(swag2*(s/3.7+1.2))-vec2(t*1.2,0.));
    float tt = mod(t,10.);
    if(tt<3.){
      fragColor = texture(iChannel0,uv);
    } else if(tt<6.){
  	  fragColor = texture(iChannel1,uv);
    } else {
      fragColor = texture(iChannel2,uv);    
    }
}

lowp vec3 permute(in lowp vec3 x) { return mod( x*x*34.+x, 289.); }
lowp float snoise(in lowp vec2 v) {
  lowp vec2 i = floor((v.x+v.y)*.36602540378443 + v),
      x0 = (i.x+i.y)*.211324865405187 + v - i;
  lowp float s = step(x0.x,x0.y);
  lowp vec2 j = vec2(1.0-s,s),
      x1 = x0 - j + .211324865405187, 
      x3 = x0 - .577350269189626; 
  i = mod(i,289.);
  lowp vec3 p = permute( permute( i.y + vec3(0, j.y, 1 ))+ i.x + vec3(0, j.x, 1 )   ),
       m = max( .5 - vec3(dot(x0,x0), dot(x1,x1), dot(x3,x3)), 0.),
       x = fract(p * .024390243902439) * 2. - 1.,
       h = abs(x) - .5,
      a0 = x - floor(x + .5);
  return .5 + 65. * dot( pow(m,vec3(4.))*(- 0.85373472095314*( a0*a0 + h*h )+1.79284291400159 ), a0 * vec3(x0.x,x1.x,x3.x) + h * vec3(x0.y,x1.y,x3.y));
}


void main(void) {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}